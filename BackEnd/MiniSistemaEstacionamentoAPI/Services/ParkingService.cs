using Microsoft.EntityFrameworkCore;
using MiniSistemaEstacionamentoAPI.Data;
using MiniSistemaEstacionamentoAPI.Enums;
using MiniSistemaEstacionamentoAPI.Models;

namespace MiniSistemaEstacionamentoAPI.Services
{
    /// <summary>
    /// Serviço responsável por toda regra de negócio
    /// relacionada à movimentação de veículos.
    /// </summary>
    public class ParkingService : IParkingInterface
    {
        private readonly ParkingDbContext _context;
        private readonly ITaxServiceRepository _taxService;
        private const decimal FirstHourPrice = 10m;
        private const decimal AdditionalHourPrice = 5m;
        
        public ParkingService(ParkingDbContext context, ITaxServiceRepository taxService)
        {
            _context = context;
            _taxService = taxService;
        }
        public async Task<ServiceResponse<ParkingSessionModel>> RegisterEntry(VehicleModel vehicle)
        {
            ServiceResponse<ParkingSessionModel> serviceResponse = new ServiceResponse<ParkingSessionModel>();

            try
            {
                if (vehicle == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Vehicle Data can't be null";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // verifica se veículo já existe pela placa para obter o ID real
                var veiculoExistente = await _context.Vehicles
                    .FirstOrDefaultAsync(x => x.Plate == vehicle.Plate);

                if (veiculoExistente == null)
                {
                
                    _context.Vehicles.Add(vehicle);
                    await _context.SaveChangesAsync();
                    veiculoExistente = vehicle;
                }
              
                bool veiculoJaNoPatio = await _context.ParkingSessions
                    .AnyAsync(x => x.VehicleId == veiculoExistente.Id && x.ExitTime == null);

                if (veiculoJaNoPatio)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "This vehicle already has an open session.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                ParkingSessionModel novaSessao = new ParkingSessionModel(veiculoExistente.Id);
                novaSessao.EntryTime = DateTime.Now;

                _context.ParkingSessions.Add(novaSessao);
                await _context.SaveChangesAsync();

                novaSessao.Vehicle = veiculoExistente;

                serviceResponse.Dados = novaSessao;
                serviceResponse.Mensagem = "Vehicle entry registered successfully!";
            }
            catch (Exception ex)
            {
                // InnerException a verificar campo obrigatório no banco
                serviceResponse.Mensagem = "Error while registering entry: " + (ex.InnerException?.Message ?? ex.Message);
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<ParkingSessionModel>>> GetActiveSessions()
        {
            // declarando meu response para retorno
            ServiceResponse<List<ParkingSessionModel>> serviceResponse = new ServiceResponse<List<ParkingSessionModel>>();

            try
            {
                // Buscando todas as sessoes ativas atraves da composição de objetos entre sessao do estacionamento e o veiculo com o O Include(x => x.Vehicle) 
                List<ParkingSessionModel> sessoesAtivas = await _context.ParkingSessions
                    .Include(x => x.Vehicle)
                    .Where(x => x.ExitTime == null)
                    .ToListAsync();

                serviceResponse.Dados = sessoesAtivas;

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "There are no vehicles in the parking lot at the moment.";
                }
                else
                {
                    serviceResponse.Mensagem = "Parking lot vehicle list recupered successfully.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<ParkingSessionModel>>> GetActiveSessionsByPlate(string plate)
        {
            ServiceResponse<List<ParkingSessionModel>> serviceResponse = new ServiceResponse<List<ParkingSessionModel>>();

            try
            {
                // Filtra apenas onde o ExitTime é null E onde a placa do veículo contém o texto digitado (independente de maiúsculo/minúsculo)
                List<ParkingSessionModel> sessoesFiltradas = await _context.ParkingSessions
                    .Include(x => x.Vehicle)
                    .Where(x => x.ExitTime == null && x.Vehicle.Plate.ToUpper().Contains(plate.ToUpper()))
                    .ToListAsync();

                serviceResponse.Dados = sessoesFiltradas;

                if (sessoesFiltradas.Count == 0)
                {
                    serviceResponse.Mensagem = "No vehicle found in the parking lot with this plate.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = "Error while searching vehicles " + ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        // privado porque a logica de processar a fatura é interna
        private void ProcessInvoice(ParkingSessionModel session)
        {
            // Calcula a duração total da sessão em horas, arredondando para cima
            TimeSpan duracao = session.ExitTime.Value - session.EntryTime;
            decimal totalHours = (decimal)Math.Ceiling(duracao.TotalHours);

            decimal vehicleFactor = session.Vehicle.Type switch
            {
                VehicleType.Moto => 0.5m,
                VehicleType.Caminhao => 2.0m,
                VehicleType.Carro => 1.0m,
                _ => 1.0m
            };

            decimal basicPayment = (totalHours <= 1)
                ? FirstHourPrice * vehicleFactor
                : (FirstHourPrice + ((totalHours - 1) * AdditionalHourPrice)) * vehicleFactor;

            decimal taxValue = _taxService.CalculateTax(basicPayment);

            // Atribui a nova instância de Invoice à composição da sessão
            session.Invoice = new Invoice(basicPayment, taxValue);
        }
        public async Task<ServiceResponse<ParkingSessionModel>> RegisterExit(int vehicleId)
        {
            ServiceResponse<ParkingSessionModel> serviceResponse = new ServiceResponse<ParkingSessionModel>();

            try
            {
                // Busca a sessão ativa pelo ID que veio do parâmetro
                ParkingSessionModel sessao = await _context.ParkingSessions
                    .Include(x => x.Vehicle)
                    .FirstOrDefaultAsync(x => x.VehicleId == vehicleId && x.ExitTime == null);

                if (sessao == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "No open session was found for vehicle";
                    serviceResponse.Sucesso = false;
                    return serviceResponse; 
                }

                //  Registra o horário exato da saída
                sessao.ExitTime = DateTime.Now;

                // processa a fatura com base na duração da sessão e no tipo do veículo, atribuindo a nova instância de Invoice à composição da sessão
                this.ProcessInvoice(sessao);

                _context.Update(sessao);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = sessao;
                serviceResponse.Mensagem = "Exit registered and invoice generated successfully!";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = $"Error while processing exit: {ex.Message}";
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
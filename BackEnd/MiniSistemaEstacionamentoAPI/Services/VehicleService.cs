using Microsoft.EntityFrameworkCore;
using MiniSistemaEstacionamentoAPI.Data;
using MiniSistemaEstacionamentoAPI.Models;
using MiniSistemaEstacionamentoAPI.Services;

namespace MiniSistemaEstacionamentoAPI.Services
{
    public class VehicleService : IVehicleInterface
    {
        //variavel para ler o contexto do banco de dados, para isso injeto o contexto no construtor da classe
        private readonly ParkingDbContext _context;

        public VehicleService(ParkingDbContext context)
        {
            _context = context;
        }

        //    //lista de vehicle armazenada dentro do serviceResponse
        public async Task<ServiceResponse<List<VehicleModel>>> GetVehicles()
        {
            ServiceResponse<List<VehicleModel>> serviceResponse =
                new ServiceResponse<List<VehicleModel>>();

            try
            {
                var vehicles = await _context.Vehicles.ToListAsync();

                foreach (var vehicle in vehicles)
                {
                    vehicle.HasOpenSession = await _context.ParkingSessions
                        .AnyAsync(x => x.VehicleId == vehicle.Id && x.ExitTime == null);
                }

                serviceResponse.Dados = vehicles;

                if (vehicles.Count == 0)
                {
                    serviceResponse.Mensagem = "anydata not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<VehicleModel>>> CreateVehicle(VehicleModel newVehicle)
        {
            ServiceResponse<List<VehicleModel>> serviceResponse = new ServiceResponse<List<VehicleModel>>();

            try
            {
                if (newVehicle == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Vehicle can't null!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Add(newVehicle);
                await _context.SaveChangesAsync(); //colocando await com async para que espere salvar no banco e depois continuar o fluxo do metodo.

                ////Atribuindo o retorno do método GetVehicles para a listUpdate
                ServiceResponse<List<VehicleModel>> listUpdate = await GetVehicles();

                serviceResponse.Dados = listUpdate.Dados;
                serviceResponse.Mensagem = "Vehicle created sucessfull";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.InnerException?.Message ?? ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<VehicleModel>>> UpdateVehicle(VehicleModel editedVehicle)
        {
            ServiceResponse<List<VehicleModel>> serviceResponse = new ServiceResponse<List<VehicleModel>>();

            try
            {
                VehicleModel vehicle = _context.Vehicles.AsNoTracking().FirstOrDefault(x => x.Id == editedVehicle.Id);

                if (editedVehicle == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Vehicle can't null!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Vehicles.Update(editedVehicle);
                await _context.SaveChangesAsync();

                ////Atribuindo o retorno do método GetVehicles para a listUpdate
                ServiceResponse<List<VehicleModel>> listUpdate = await GetVehicles();

                serviceResponse.Dados = listUpdate.Dados;
                serviceResponse.Mensagem = "Vehicle updated sucessfull";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

        }
        public async Task<ServiceResponse<VehicleModel>> GetVehicle(int id)
        {
            ServiceResponse<VehicleModel> serviceResponse =
                new ServiceResponse<VehicleModel>();

            try
            {
                var vehicle = await _context.Vehicles
                    .FirstOrDefaultAsync(v => v.Id == id);

                if (vehicle == null)
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Vehicle not found";
                    return serviceResponse;
                }

                serviceResponse.Dados = vehicle;
            }
            catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }


    }
}




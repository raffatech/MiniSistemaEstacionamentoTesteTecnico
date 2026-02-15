using Microsoft.AspNetCore.Mvc;
using MiniSistemaEstacionamentoAPI.Models;
using MiniSistemaEstacionamentoAPI.Services;

namespace MiniSistemaEstacionamentoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSessionController : ControllerBase
    {
        private readonly IParkingInterface _parkingInterface;

        public ParkingSessionController(IParkingInterface parkingInterface)
        {
            _parkingInterface = parkingInterface;
        }

        //Lista todos os veículos que estão no pátio 
        [HttpGet("GetActiveSessions")]
        public async Task<ActionResult<ServiceResponse<List<ParkingSessionModel>>>> GetActiveSessions()
        {
            return Ok(await _parkingInterface.GetActiveSessions());
        }

        // Busca os veiculos por placa
        [HttpGet("GetByPlate/{plate}")]
        public async Task<ActionResult<ServiceResponse<List<ParkingSessionModel>>>> GetActiveSessionsByPlate(string plate)
        {
            return Ok(await _parkingInterface.GetActiveSessionsByPlate(plate));
        }

        // Criando a sessao de apos entrada do veiculo e regra Um veículo não pode entrar se já estiver “no pátio” (sessão aberta
        [HttpPost("RegisterEntry")]
        public async Task<ActionResult<ServiceResponse<ParkingSessionModel>>> RegisterEntry(VehicleModel vehicle)
        {
            return Ok(await _parkingInterface.RegisterEntry(vehicle));
        }

        // Registra a saída, calcula valor e encerra a sessão, Um veículo não pode sair se não tiver sessão aberta
        [HttpPut("RegisterExit/{vehicleId}")]
        public async Task<ActionResult<ServiceResponse<ParkingSessionModel>>> RegisterExit(int vehicleId)
        {
            return Ok(await _parkingInterface.RegisterExit(vehicleId));
        }
    }
}
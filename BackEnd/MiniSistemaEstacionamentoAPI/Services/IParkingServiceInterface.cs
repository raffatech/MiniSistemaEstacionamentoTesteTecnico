using Microsoft.EntityFrameworkCore;
using MiniSistemaEstacionamentoAPI.Data;
using MiniSistemaEstacionamentoAPI.Enums;
using MiniSistemaEstacionamentoAPI.Models;

namespace MiniSistemaEstacionamentoAPI.Services
{
    public interface IParkingInterface
    {
        // Lista veículos no pátio (Sessões onde ExitTime é null)
        Task<ServiceResponse<List<ParkingSessionModel>>> GetActiveSessions();

        // Busca por placa (para o filtro da tela)
        Task<ServiceResponse<List<ParkingSessionModel>>> GetActiveSessionsByPlate(string plate);

        // Registrar entrada
        Task<ServiceResponse<ParkingSessionModel>> RegisterEntry(VehicleModel vehicle);

        // Registrar saída (Calcula valor e encerra sessão)
        Task<ServiceResponse<ParkingSessionModel>> RegisterExit(int vehicleId);
    }
}

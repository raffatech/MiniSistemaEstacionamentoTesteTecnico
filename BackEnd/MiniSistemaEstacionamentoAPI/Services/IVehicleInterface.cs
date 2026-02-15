using MiniSistemaEstacionamentoAPI.Models;

namespace MiniSistemaEstacionamentoAPI.Services
{
    public interface IVehicleInterface
    {
        //usando task para usar metodos assincronos (aguardar a requisição do banco antes de seguir com o metodo)

        Task<ServiceResponse<List<VehicleModel>>> GetVehicles();
        Task<ServiceResponse<List<VehicleModel>>> CreateVehicle(VehicleModel newVehicle);
        Task<ServiceResponse<List<VehicleModel>>> UpdateVehicle(VehicleModel editedVehicle);
        Task<ServiceResponse<VehicleModel>> GetVehicle(int id);

    }
}

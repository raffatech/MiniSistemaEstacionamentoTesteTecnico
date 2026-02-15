using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSistemaEstacionamentoAPI.Models;
using MiniSistemaEstacionamentoAPI.Services;

namespace MiniSistemaEstacionamentoAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleInterface _vehicleInterface;
        public VehicleController(IVehicleInterface vehicleInterface)
        {
            this._vehicleInterface = vehicleInterface;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<VehicleModel>>>> GetVehicles()
        {
            return Ok(await _vehicleInterface.GetVehicles());
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<VehicleModel>>>> CreateVehicle(VehicleModel newVehicle)
        {
            return Ok(await _vehicleInterface.CreateVehicle(newVehicle));
        }
        [HttpPut]
        public async Task<ActionResult<List<VehicleModel>>> UpdateVehicle(VehicleModel editedVehicle)
        {
            ServiceResponse<List<VehicleModel>> serviceResponse = await _vehicleInterface.UpdateVehicle(editedVehicle);

            return Ok(serviceResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<VehicleModel>>> GetVehicle(int id)
        {
            return Ok(await _vehicleInterface.GetVehicle(id));
        }
    }
}

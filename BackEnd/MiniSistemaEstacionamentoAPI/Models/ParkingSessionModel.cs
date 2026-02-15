using System.ComponentModel.DataAnnotations;

namespace MiniSistemaEstacionamentoAPI.Models
{
    /// <summary>
    /// Representa uma movimentação de entrada e saída de veículo.
    /// Se ExitTime for null, a sessão está aberta.
    /// </summary>
    public class ParkingSessionModel
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Data e hora da entrada do veículo.
        /// </summary>
        [Required]
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// Data e hora da saída.
        /// Null significa sessão aberta.
        /// </summary>
        public DateTime? ExitTime { get; set; }

        /// <summary>
        /// Chave estrangeira para o veículo.
        /// </summary>
        [Required]
        public int VehicleId { get; set; }

        public VehicleModel Vehicle { get; set; }

        /// <summary>
        /// Resultado financeiro da sessão.
        /// </summary>
        public Invoice Invoice { get; set; }

        public ParkingSessionModel() { }

        public ParkingSessionModel(int vehicleId)
        {
            VehicleId = vehicleId;
            EntryTime = DateTime.UtcNow; 
        }

        /// <summary>
        /// Indica se a sessão está aberta.
        /// </summary>
        public bool IsOpen()
        {
            return ExitTime == null;
        }
    }
}

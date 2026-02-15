using MiniSistemaEstacionamentoAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSistemaEstacionamentoAPI.Models
{
    /// <summary>
    /// Representa um veículo cadastrado no sistema.
    /// A placa é única no banco de dados.
    /// </summary>
    public class VehicleModel
    {
        [Key]
        public int Id { get; set;}

        /// <summary>
        /// Placa do veículo.
        /// Obrigatória e única.
        /// </summary>
        [Required(ErrorMessage = "Plate is required")]
        [MaxLength(10)]
        public string Plate { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        /// <summary>
        /// Tipo do veículo utilizado para regra de precificação.
        /// </summary>
        [Required]
        public VehicleType Type { get; set; }

        [NotMapped]
        public bool HasOpenSession { get; set; }

        public VehicleModel() { }

        public VehicleModel(string plate, string model, string color, VehicleType type)
        {
            Plate = plate;
            Model = model;
            Color = color;
            Type = type;
        }
    }
}

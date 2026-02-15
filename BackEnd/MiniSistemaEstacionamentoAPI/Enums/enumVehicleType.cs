using System.Text.Json.Serialization;

namespace MiniSistemaEstacionamentoAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))] //transformar o numero para texto dentro da API para ficar mais amigavel
    /// <summary>
    /// Enum utilizado para diferenciar tipos de veículos
    /// e aplicar regras de precificação.
    /// </summary>
    public enum VehicleType
    {
        Carro = 1,
        Moto = 2,
        Caminhao = 3
    }
}

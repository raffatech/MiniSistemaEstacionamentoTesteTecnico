namespace MiniSistemaEstacionamentoAPI.Services
{
    /// <summary>
    /// Interface que define cálculo de imposto.
    /// Permite aplicar diferentes regras para Brasil e Argentina.
    /// </summary>
    public interface ITaxServiceRepository
    {
        decimal CalculateTax(decimal amount);
    }
}

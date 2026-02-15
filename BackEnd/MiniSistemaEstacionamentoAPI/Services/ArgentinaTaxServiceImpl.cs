namespace MiniSistemaEstacionamentoAPI.Services
{
    /// <summary>
    /// Implementação de cálculo de imposto para Argentina.
    /// Regra fictícia apenas para simulação do desafio.
    /// </summary>
    public class ArgentinaTaxService : ITaxServiceRepository
    {
        public decimal CalculateTax(decimal amount)
        {
            if (amount <= 50m) {
                return amount * 0.3m;
            }
            return amount * 0.5m;
        }
    }
}

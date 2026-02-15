namespace MiniSistemaEstacionamentoAPI.Services
{
    /// <summary>
    /// Implementação de cálculo de imposto para o Brasil.
    /// Regra fictícia apenas para simulação do desafio.
    /// </summary>
    public class BrazilTaxService : ITaxServiceRepository
    {
        public decimal CalculateTax(decimal amount)
        {
            if (amount <= 100m) { 
                return amount * 0.2m;
        }
            return amount * 0.5m;
        }
    }
}

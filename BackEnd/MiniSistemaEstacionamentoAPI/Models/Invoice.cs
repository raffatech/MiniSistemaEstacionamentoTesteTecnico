using Microsoft.EntityFrameworkCore;

namespace MiniSistemaEstacionamentoAPI.Models
{
    /// Representa o resultado financeiro de uma sessão.
    /// É uma entidade "Owned" do Entity Framework,
    /// ou seja, não possui tabela própria.
    [Owned]
    public class Invoice
    {
        public decimal? BasicPayment { get; set; }
        public decimal? Tax { get; set; }

        public Invoice() { }
        public Invoice(decimal basicPayment, decimal tax)
        {
            BasicPayment = basicPayment;
            Tax = tax;
        }

        public decimal TotalPayment() => (BasicPayment ?? 0) + (Tax ?? 0);
    }
}

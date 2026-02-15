namespace MiniSistemaEstacionamentoAPI.Models
{
    //Classe para mensagem de sucesso dos dados
    public class ServiceResponse<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; }

        public bool Sucesso { get; set; } = true;
    }
}

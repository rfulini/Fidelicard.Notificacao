namespace Fidelicard.Notificacao.Core.Models
{
    public class Notificacoes
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}

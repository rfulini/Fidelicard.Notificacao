using Fidelicard.Notificacao.Core.Models;

namespace Fidelicard.Notificacao.Core.Result
{
    public class NotificacaoResult
    {
        public NotificacaoStatus Status { get; protected set; }

        public Notificacoes Notificacoes { get; protected set; }

        public Exception ErroProcessamento { get; protected set; }

        public string Mensagem { get; protected set; }

        protected NotificacaoResult(NotificacaoStatus status, Notificacoes notificacoes, Exception erroProcessamento, string mensagem)
        {
            Status = status;
            Notificacoes = notificacoes;
            ErroProcessamento = erroProcessamento;
            Mensagem = mensagem;
        }

        public static NotificacaoResult SucessoObterNotificacao(Notificacoes notificacoes) =>
            new NotificacaoResult(NotificacaoStatus.SucessoObterNotificacao, notificacoes, null, string.Empty);

        public static NotificacaoResult SucessoEnviaNotificacao(Notificacoes notificacoes) =>
            new NotificacaoResult(NotificacaoStatus.SucessoEnviarNotificacao, notificacoes, null, string.Empty);

        public static NotificacaoResult DadosInvalidos(Exception erroProcessamento, string mensagem) =>
           new NotificacaoResult(NotificacaoStatus.DadosInvalidos, null, erroProcessamento, mensagem);

        public static NotificacaoResult ErroObterNotificacao(Exception erroProcessamento, string mensagem) =>
            new NotificacaoResult(NotificacaoStatus.ErroObterNotificacao, null, erroProcessamento, mensagem);
    }
}

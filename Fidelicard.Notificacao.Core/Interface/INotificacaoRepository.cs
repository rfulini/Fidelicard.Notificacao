using Fidelicard.Notificacao.Core.Models;
using Fidelicard.Notificacao.Core.Result;
using System.Data;

namespace Fidelicard.Notificacao.Core.Interface
{
    public interface INotificacaoRepository
    {
        Task<NotificacaoResult> ObterNotificacaoAsync(int idNotificacao);
        Task<NotificacaoResult> EnviarNotificacaoAsync(Notificacoes notificacoes);

        IDbConnection GetConnection();
    }
}

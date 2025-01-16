using Fidelicard.Notificacao.Core.Models;
using Fidelicard.Notificacao.Core.Result;

namespace Fidelicard.Notificacao.Core.Interface
{
    public interface INotificacaoService
    {
        Task<NotificacaoResult> EnviarNotificacaoUsuarioAsync(Notificacoes notificacoes);

        Task<NotificacaoResult> ConsultarNotificacaoAsync(int idNotificacao);

    }
}

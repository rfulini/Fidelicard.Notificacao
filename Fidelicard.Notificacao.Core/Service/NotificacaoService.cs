using Fidelicard.Notificacao.Core.Interface;
using Fidelicard.Notificacao.Core.Models;
using Fidelicard.Notificacao.Core.Result;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;

namespace Fidelicard.Notificacao.Core.Service
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;
        private readonly ILogger<NotificacaoService> _logger;
        private const string TeamsWebhookUrl = "https://fiapcom.webhook.office.com/webhookb2/fb2a5d75-bf78-4540-a8e2-8da96f0161ba@11dbbfe2-89b8-4549-be10-cec364e59551/IncomingWebhook/6f232dc52dfe410da32f4665338261b5/0eaa375e-b457-47d7-9870-519e14bb9e31/V2_soaPGHEu15Z3m2W0JlGRT9CAMqy5wfWCeWZBbiIW0M1";
        public NotificacaoService(ILogger<NotificacaoService> logger
        , INotificacaoRepository notificacaoRepository)
        {
            _logger = logger;
            _notificacaoRepository = notificacaoRepository;
        }

        public async Task<NotificacaoResult> ConsultarNotificacaoAsync(int idNotificacao)
        {
            _logger.LogInformation("Iniciando consulta da notificação com Id: {idNotificacao}", idNotificacao);

            try
            {
                var notificacao = await _notificacaoRepository.ObterNotificacaoAsync(idNotificacao).ConfigureAwait(false);

                if (notificacao == null)
                {
                    var mensagem = $"Notificação inexistente pelo código informado: {idNotificacao}";
                    _logger.LogWarning(mensagem);
                    return NotificacaoResult.DadosInvalidos(new Exception(mensagem), mensagem);
                }

                _logger.LogInformation("Consulta da notificação com Id: {idNotificacao} concluída com sucesso.", idNotificacao);
                return NotificacaoResult.SucessoObterNotificacao(notificacao.Notificacoes);
            }
            catch (Exception ex)
            {
                var mensagemErro = $"Erro ao consultar a notificação com Id: {idNotificacao}.";
                _logger.LogError(ex, mensagemErro);
                return NotificacaoResult.ErroObterNotificacao(ex, mensagemErro);
            }
        }

        public async Task<NotificacaoResult> EnviarNotificacaoUsuarioAsync(Notificacoes notificacoes)
        {
            _logger.LogInformation("Iniciando envio da notificação do {Id} com a mensagem: {Messagem}", notificacoes.Id, notificacoes.Mensagem);

            try
            {
                var payload = new
                {
                    text = notificacoes.Mensagem
                };

                using var httpClient = new HttpClient();
                var jsonPayload = JsonSerializer.Serialize(payload);
                var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(TeamsWebhookUrl, httpContent);

                _logger.LogInformation("Noitificaçao {Id} enviada com a mensagem: {Mensagem} com sucesso.", notificacoes.Id, notificacoes.Mensagem);
                return NotificacaoResult.SucessoEnviaNotificacao(notificacoes);
            }
            catch (Exception ex)
            {
                var mensagemErro = $"Erro ao consultar a notificação com Id: {notificacoes.Id}.";
                _logger.LogError(ex, mensagemErro);
                return NotificacaoResult.ErroObterNotificacao(ex, mensagemErro);
            }
        }
    }
}

using Fidelicard.Notificacao.Core.Interface;
using Fidelicard.Notificacao.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fidelicard.Notificacao.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/fidelicard/notificacao")]
    public class NotificacaoController : Controller
    {
        private readonly INotificacaoService _service;
        private readonly ILogger<NotificacaoController> _logger;
        private readonly IConfiguration _configuration;

        public NotificacaoController(INotificacaoService service,
           ILogger<NotificacaoController> logger,
           IConfiguration configuration)
        {
            _service = service;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("enviar")]
        [SwaggerResponse(StatusCodes.Status200OK, "Notificação enviada com sucesso", typeof(NotificacaoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Não autorizado")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Acesso negado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor")]
        public async Task<IActionResult> EnviarNotificacao([FromBody] Notificacoes notificacao)
        {

            if (notificacao == null)
            {
                return BadRequest(new { Mensagem = "O corpo da requisição não pode ser nulo." });
            }

            try
            {
                var response = await _service.EnviarNotificacaoUsuarioAsync(notificacao).ConfigureAwait(false);

                if (response == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { Mensagem = "Erro ao processar o envio da mensagem. Tente novamente mais tarde." });
                }

                return Ok(response);
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning("Validação falhou ao enviar a mensagem: {Message}", argEx.Message);
                return BadRequest(new { Mensagem = argEx.Message });
            }
            catch (UnauthorizedAccessException authEx)
            {
                _logger.LogWarning("Falha de autenticação: {Message}", authEx.Message);
                return Unauthorized(new { Mensagem = "Acesso não autorizado." });
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro inesperado ao enviar a mensagem: {Message} - STACKTRACE: {StackTrace}", ex.Message, ex.StackTrace);

                var errorDetails = new
                {
                    Mensagem = "Erro inesperado ao processar sua solicitação. Tente novamente mais tarde.",
                    Controle = new
                    {
                        Codigo = "USUARIO.500",
                        Descricao = "Erro no processamento de enviar a mensagem"
                    }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorDetails);
            }
        }
    }
}

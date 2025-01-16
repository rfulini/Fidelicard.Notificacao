using AutoMapper;
using Fidelicard.Notificacao.Core.Interface;
using Fidelicard.Notificacao.Core.Models;
using Fidelicard.Notificacao.Core.Result;
using Fidelicard.Notificacao.Infra.Config;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Fidelicard.Notificacao.Infra.Repository
{
    public class NotificacaoRepository : BaseRepository, INotificacaoRepository
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _connectionString;

        public NotificacaoRepository(IDatabaseContext context,
            ILogger<NotificacaoRepository> logger,
            IMapper mapper,
            IConfiguration configuration) : base(context)
        {
            _logger = logger;
            _mapper = mapper;
            _connectionString = configuration.GetSection("ConnectionStrings:DBUsuario").Value;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public Task<NotificacaoResult> EnviarNotificacaoAsync(Notificacoes notificacoes)
        {
            throw new NotImplementedException();
        }

        public Task<NotificacaoResult> ObterNotificacaoAsync(int idNotificacao)
        {
            throw new NotImplementedException();
        }
    }
}

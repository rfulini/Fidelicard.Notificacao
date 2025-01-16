using System.Data;

namespace Fidelicard.Notificacao.Infra.Config
{
    public interface IDatabaseContext
    {
        IDbConnection GetConnection();
    }
}

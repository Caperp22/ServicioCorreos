using System.Data;

namespace DataExpress.RecepcionCorreo.Infraestructura.Interface
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
        IDbConnection GetReceiveConnection { get; }
    }
}

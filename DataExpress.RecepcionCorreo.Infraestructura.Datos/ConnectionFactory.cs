using DataExpress.RecepcionCorreo.Infraestructura.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace DataExpress.RecepcionCorreo.Infraestructura.Datos
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration) => _configuration = configuration;

        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;
                sqlConnection.ConnectionString = _configuration.GetConnectionString("DBConnnection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }

        public IDbConnection GetReceiveConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;
                sqlConnection.ConnectionString = _configuration.GetConnectionString("DBReceiveConnnection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
    }
}

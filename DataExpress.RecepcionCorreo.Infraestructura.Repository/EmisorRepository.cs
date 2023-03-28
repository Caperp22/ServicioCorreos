using Dapper;
using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Infraestructura.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataExpress.RecepcionCorreo.Infraestructura.Repository
{
    public class EmisorRepository : IEmisorRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public EmisorRepository(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public EmisorDto ObtenerDatosEmisor(string identificacion)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = $"SELECT NOMEMI As RazonSocial, RFCEMI As NIT,  PersonaFiscal As TipoPersona FROM Cat_Emisor WHERE RFCEMI = '{identificacion}'" ;
                return connection.QuerySingle<EmisorDto>(query);
            }
        }
    }
}

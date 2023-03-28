using Dapper;
using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Infraestructura.Interface;
using System;
using System.Text.RegularExpressions;

namespace DataExpress.RecepcionCorreo.Infraestructura.Repository
{
    public class AcuseRepository : IAcuseRepository
    {
        private IConnectionFactory _connectionFactory;

        public AcuseRepository(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public string ConsultarNitAcuse(string cufe)
        {
            using (var connection = _connectionFactory.GetReceiveConnection)
            {
                var query = $@"select rfcrec from dat_general g, cat_receptor r where r.iderec = g.id_receptor and  g.ClaveAcceso= '{cufe}'";
                return connection.QuerySingle<string>(query);
            }
        }

        public string GuardarAcuase(DatosGeneralDto datos, string idComprobante)
        {
            var date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = $@"insert into Dat_AcuseR (idcomprobante,fecha,ip,email,observaciones,CodAcuse) values 
                ('{idComprobante}','{date}','','','{datos.Observciones}','{datos.Tipo}'); SELECT SCOPE_IDENTITY() AS SCOPE_IDENTITY; ";
                var id =connection.QuerySingle<string>(query);
                return id;
            }
        }

        public string ObtenerComprobante(string cufe)
        {
            using (var connection = _connectionFactory.GetReceiveConnection)
            {
                var query = $@"Select idComprobante from dat_general Where ClaveAcceso = '{cufe}'";
                return connection.QuerySingle<string>(query);
            }
        }

        public FechasDto ObtenerFechas(string idComprobante)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = $@"SELECT fecha As FechaEmiRef, fechaVencimiento as FechaVenRef FROM Dat_General where idComprobante={idComprobante}";
                return connection.QuerySingle<FechasDto>(query);
            }
        }

        public string ObtenerSerie()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = $@"select serie from cat_series where tipoDoc ='14'";
                return connection.QuerySingle<string>(query);
            }
        }

        public AcuseDto ObtenerSerieFolio(string idComprobante)
        {
            using (var connection = _connectionFactory.GetReceiveConnection)
            {
                var query = $@"Select Serie as Serie, folio as Folio,codDoc as TipoComprobante from Dat_General where idComprobante={idComprobante}";
                return connection.QuerySingle<AcuseDto>(query);
            }
        }
        public AceptanteDto ObtenerAceptante(string user)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = $@"SELECT idEmpleado as Id,nombreEmpleado as Nombre, apellido as Apellidos
                                ,'31' as TipoDocumento, cedula as Identificacion,0 as DV,'13' as TipoPersona, areaDepartamento as Area
                                ,cargo as Cargo, descripcion as Grupo,orden as Nivel 
                            FROM {connection.Database}.dbo.Cat_Empleados emp
                            INNER JOIN {connection.Database.Replace("Emision","Recepcion")}.dbo.Cat_Grupos_validadores rol 
                                ON emp.idGrupo = rol.idGrupo
                            WHERE userEmpleado = '{user}'";
                return connection.QuerySingle<AceptanteDto>(query);
            }
        }
        public bool ExisteValidacion(string idComprobante,string idEmpleado) {
            using (var connection = _connectionFactory.GetReceiveConnection)
            {
                var query = $@"SELECT COUNT(*) FROM Dat_Validaciones WHERE id_Comprobante = {idComprobante} AND idEmpleadoEmision = {idEmpleado}";
                var count=connection.QuerySingle<int>(query);
                return count > 0 ? true : false;
            }

        }
        public void GuardarValidacion(AceptanteDto aceptante,string idComprobante,string status,string observaciones) {

            using (var connection = _connectionFactory.GetReceiveConnection)
            {
                var query = $@"INSERT INTO Dat_Validaciones (id_Comprobante, idEmpleadoEmision, estadoValidacion, observaciones) 
                                VALUES ('{idComprobante}', '{aceptante.Id}', '{status}', '{observaciones}'); 
                               SELECT SCOPE_IDENTITY() AS SCOPE_IDENTITY;  ";
                var id =  connection.QuerySingle<string>(query);

                query = $@"INSERT INTO Dat_Relacion_Validaciones (idComprobante, Empleado, Grupo, estatus,nivel,fecha_validacion) 
                            VALUES ('{idComprobante}', '{aceptante.Nombre}', '{aceptante.Grupo}', '{status}', '{aceptante.Nivel}', GETDATE()); 
                              SELECT SCOPE_IDENTITY() AS SCOPE_IDENTITY; ";
                 id = connection.QuerySingle<string>(query);
                if (observaciones.Equals("ACEPTADO")) {
                    query = $@"UPDATE Dat_General SET estadoValidacion =1 WHERE idComprobante = {idComprobante} ";
                    connection.Query(query);
                }
            }

        }
    }
}

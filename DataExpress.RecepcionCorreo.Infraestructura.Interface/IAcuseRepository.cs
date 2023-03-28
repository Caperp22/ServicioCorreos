using DataExpress.RecepcionCorreo.Aplicacion.DTO;

namespace DataExpress.RecepcionCorreo.Infraestructura.Interface
{
    public interface IAcuseRepository
    {
        string ObtenerSerie();
        string ObtenerComprobante(string cufe);
        FechasDto ObtenerFechas(string idComprobante);
        string GuardarAcuase(DatosGeneralDto datos, string idComprobante);
        AcuseDto ObtenerSerieFolio(string idComprobante);
        string ConsultarNitAcuse(string cufe);
        AceptanteDto ObtenerAceptante(string user);
        bool ExisteValidacion(string idComprobante, string idEmpleado);
        void GuardarValidacion(AceptanteDto aceptante, string idComprobante, string status, string observaciones);
    }
}

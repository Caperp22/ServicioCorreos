using DataExpress.RecepcionCorreo.Aplicacion.DTO;

namespace DataExpress.RecepcionCorreo.Infraestructura.Interface
{
    public interface IEmisorRepository
    {
        EmisorDto ObtenerDatosEmisor(string identificacion);
    }
}

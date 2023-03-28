using DataExpress.RecepcionCorreo.Aplicacion.Interface;

namespace DataExpress.RecepcionCorreo.Servicio.Acuses.LogicaNegocio
{
    public class AcusesAceptacion
    {
        IAcuse _acuse;
        public AcusesAceptacion(IAcuse acuse) => _acuse = acuse;

        public void InicioAcuse() => _acuse.CrearAcuse();
    }
}

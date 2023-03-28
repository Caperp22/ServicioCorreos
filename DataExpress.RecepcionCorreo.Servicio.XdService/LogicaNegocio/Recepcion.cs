using DataExpress.RecepcionCorreo.Aplicacion.Interface;

namespace DataExpress.RecepcionCorreo.Servicio.XdService.LogicaNegocio
{
    public class Recepcion
    {
        private readonly ILeerCorreo _correo;


        public Recepcion(ILeerCorreo correo) => _correo = correo;


        public void InicioLecturaCorreo() => _correo.RecibirCorreo();

    }
}

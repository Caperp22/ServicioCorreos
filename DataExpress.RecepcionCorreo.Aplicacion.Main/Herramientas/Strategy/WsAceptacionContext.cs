using DataExpress.RecepcionCorreo.Aplicacion.Interface;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class WsAceptacionContext
    {
        private IWsAceptacion _wsAceptacion;
        public WsAceptacionContext(IWsAceptacion wsAceptacion) => _wsAceptacion = wsAceptacion;
        public void  AcpetacionDocumentos() => _wsAceptacion.AceptarDocumento();

    }
}

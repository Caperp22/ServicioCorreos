using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class WsContext
    {
        private IWsRecepcionStrategy _wsRecepcion;

        public WsContext(IWsRecepcionStrategy wsRecepcion) => _wsRecepcion = wsRecepcion;

        public void RecepcionDocumentos() => _wsRecepcion.RecepcionDocumentos();
    }
}

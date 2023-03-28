using System;
using System.Collections.Generic;
using System.Text;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Builder
{
    public class JsonDirector
    {
        private IComprobanteJson _comprobanteJson;

        public JsonDirector(IComprobanteJson comprobanteJson)
        {
            _comprobanteJson = comprobanteJson;
        }

        public void GenerarJson(string Identificacion)
        {
            _comprobanteJson.Reiniciar(Identificacion);
            _comprobanteJson.DatosGenerales();
            _comprobanteJson.CUDE();
            _comprobanteJson.Receptor();
            _comprobanteJson.Eventos();
        }
    }
}

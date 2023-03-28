using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using Newtonsoft.Json;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Builder
{
    public class JsonContext
    {
        public ObjDto comprobante;
        public string JsonResult;

        public void GenerarJson() => JsonResult = JsonConvert.SerializeObject(comprobante);

        public JsonContext() => comprobante = new ObjDto();

    }
}

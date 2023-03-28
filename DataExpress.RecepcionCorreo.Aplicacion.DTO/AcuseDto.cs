using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DataExpress.RecepcionCorreo.Aplicacion.DTO
{
    public class AcuseDto
    {
        public string Folio { get; set; }
        public string Serie { get; set; }
        public string TipoComprobante { get; set; } 
    }

    public class DatosGeneralDto
    {
        long _tipoPersonaProveedor = 1;
        public AceptanteDto Aceptante { get; set; }
        public EmisorDto Emisor { get; set; }
        public string Tipo { get; set; }/*Código acuse*/
        public string Factor { get; set; }
        public long TipoPersonaProveedor { get { return _tipoPersonaProveedor; } set { _tipoPersonaProveedor = value; } }
        public AttachedDocumentType Attached { get; set; }
        public XmlDocument Xml { get; set; }
        public string Serie { get; set; }
        public string Observciones { get; set; }
    }
}

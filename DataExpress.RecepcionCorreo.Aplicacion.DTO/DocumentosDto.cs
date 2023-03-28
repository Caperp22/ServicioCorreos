using System;
using System.Collections.Generic;

namespace DataExpress.RecepcionCorreo.Aplicacion.DTO
{
    public class DocumentosDto
    {
        public string asunto { get; set; }
        public string email { get; set; }
        public DateTime recibido { get; set; }
        public string nombreZip { get; set; } 
        public int id { get; set; }
        public string nombreDocumento { get; set; }
        public byte[] documentos { get; set; }
        public string nombre2 { get; set; }
        public string estatus { get; set; }
    }
}

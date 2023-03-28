using System;
using System.Collections.Generic;
using System.Text;

namespace DataExpress.RecepcionCorreo.Aplicacion.DTO
{
    public class ProveedorDto:EmisorDto
    {
    }
    public class EmisorDto
    {
        public string NIT { get; set; }
        public string RazonSocial { get; set; }
        public long TipoPersona { get; set; }
        public long TipoDocumento { get; set; }
        public string Factor { get; set; }
    }

    public class AceptanteDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public long TipoDocumento { get; set; }
        public string Identificacion { get; set; }
        public string DV { get; set; }
        public long TipoPersona { get; set; }
        public string Area { get; set; }
        public string Cargo { get; set; }
        public string Grupo { get; set; }
        public string Nivel { get; set; }
    }
    public class AceptantesConfig
    {
        public List<AceptanteConfig> List { get; set; }
    }
    public class AceptanteConfig
    {
        public string User { get; set; }
        public string Level { get; set; }

    }
}

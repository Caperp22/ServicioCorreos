using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataExpress.RecepcionCorreo.Aplicacion.Interface
{
    public interface IFileStrategy
    {
        public List<DocumentosDto> lstMail { get; set; }
        void LeerArchivos();
    }
}

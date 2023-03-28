using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataExpress.RecepcionCorreo.Aplicacion.Interface
{
    public interface ICorreoStrategy
    {
        public List<DocumentosDto> lstMail { get; set; }
        void LeerCorreo();
    }
}

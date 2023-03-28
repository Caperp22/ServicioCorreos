using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using System.Collections.Generic;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class CorreoContext
    {
        private ICorreoStrategy _strategy;

        public CorreoContext(ICorreoStrategy strategy) => _strategy = strategy;

        public void LeerCorreo() => _strategy.LeerCorreo();
        public List<DocumentosDto> GetList() => _strategy.lstMail;
    }
}

using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using System.Collections.Generic;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class FileContext
    {
        private IFileStrategy _strategy;

        public FileContext(IFileStrategy strategy) => _strategy = strategy;

        public void LeerArchivos() => _strategy.LeerArchivos();
        public List<DocumentosDto> GetList() => _strategy.lstMail;
    }
}

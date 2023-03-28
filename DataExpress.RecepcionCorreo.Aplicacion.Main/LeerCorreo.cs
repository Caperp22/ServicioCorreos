using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy;
using Microsoft.Extensions.Options;
using System;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main
{
    public class LeerCorreo : ILeerCorreo
    {
        private MailSettingsDto _optionsMail;
        private ConfiguracionDto _optionsConfig;
        private SmtpConfigurationDTo _optionsSmtp;

        public LeerCorreo(IOptions<MailSettingsDto> optionsMail, IOptions<ConfiguracionDto> optionsConfig, IOptions<SmtpConfigurationDTo> optionsSmtp)
        {
            _optionsMail = optionsMail.Value;
            _optionsConfig = optionsConfig.Value;
            _optionsSmtp = optionsSmtp.Value;
        }

        public void RecibirCorreo()
        {
            try
            {
                var contextCorreo = _optionsMail.OutlookInstalled ? new CorreoContext(new OutlookStrategy(_optionsMail)) : new CorreoContext(new MailStrategy(_optionsMail));
                contextCorreo.LeerCorreo();

                var contextFile = new FileContext(new FileStrategy(_optionsConfig));
                contextFile.LeerArchivos();

                var contextWs = _optionsConfig.Pais.ToUpper().Equals("CO") ? new WsContext(new WsColombiaStrategy(contextFile.GetList(), _optionsConfig, _optionsSmtp)) : new WsContext(new WsMexicoStrategy());
                contextWs.RecepcionDocumentos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

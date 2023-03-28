using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Infraestructura.Interface;
using System.IO.Compression;
using System.IO;
using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main
{
    public class Acuse : IAcuse
    {
        private readonly IAcuseRepository _acuseRepository;
        private readonly IEmisorRepository _emisorRepository;
        //private readonly ConfiguracionDto _optionsConfig;
        private ConfiguracionDto _optionsConfig;
        private readonly SmtpConfigurationDTo _optionsSmtp;
        private MailSettingsDto _optionsMail;
        private readonly AceptantesConfig _optionsAceptantes;
        private string _directorio;
        private List<DocumentosDto> _lstMail;

        public Acuse(IAcuseRepository acuseRepository, IEmisorRepository emisorRepository, IOptions<ConfiguracionDto> optionsConfig, IOptions<SmtpConfigurationDTo> optionsSmtp, IOptions<AceptantesConfig> optionsAceptante)
        {
            _acuseRepository = acuseRepository;
            _emisorRepository = emisorRepository;
            _optionsConfig = optionsConfig.Value;
            _optionsSmtp = optionsSmtp.Value;
            _optionsAceptantes = optionsAceptante.Value;
        }

        public void CrearAcuse()
        {
            try
            {
                //var contextCorreo = _optionsMail.OutlookInstalled ? new CorreoContext(new OutlookStrategy(_optionsMail)) : new CorreoContext(new MailStrategy(_optionsMail));
                //contextCorreo.LeerCorreo();

                var contextFile = new FileContext(new FileStrategy(_optionsConfig));
                contextFile.LeerArchivos();

                var contextWs = _optionsConfig.Pais.ToUpper().Equals("CO") ? new WsContext(new WsColombiaStrategy(_lstMail, _optionsConfig, _optionsSmtp)) : new WsContext(new WsMexicoStrategy());
                contextWs.RecepcionDocumentos();

                _lstMail = contextFile.GetList();
                var contextWs2 = _optionsConfig.Pais.ToUpper().Equals("CO") ? new WsAceptacionContext(new WsAceptacionColombiaStrategy(_acuseRepository, _emisorRepository, _lstMail, _optionsConfig, _optionsSmtp, _optionsAceptantes)) : new WsAceptacionContext(new WsAceptacionMXStrategy());
                contextWs2.AcpetacionDocumentos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ExtraerZip()
        {
            try
            {
                var files = Directory.GetFiles(_optionsConfig.RutaZip+@"Zip\");

                foreach (var file in files)
                {
                    var infoFile = new FileInfo(file);
                    ZipFile.ExtractToDirectory(file, _directorio);
                    File.Delete(file);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ObtenerArchivos()
        {
            var files = Directory.GetFiles(_directorio);
            var contador = 0;
            var contador2 = 0;
            _lstMail = new List<DocumentosDto>();
            foreach (var file in files)
            {
                var infoFile = new FileInfo(file);
                if (Regex.IsMatch(infoFile.Extension.ToLower(), "pdf|xml"))
                {
                    var attach = File.ReadAllBytes(infoFile.FullName);
                    var name2 = infoFile.Name.Replace(infoFile.Extension, "");
                    name2 = name2.Substring(2);

                    var existe = _lstMail.Where(x => x.nombre2 == name2).ToList();

                    contador2 = existe.Count > 0 ? existe.Select(s => s.id).FirstOrDefault() : contador++;

                    var documento = new DocumentosDto
                    {
                        id = contador2,
                        documentos = attach,
                        nombreDocumento = infoFile.Name,
                        nombre2 = name2
                    };
                    _lstMail.Add(documento);
                    File.Delete(file);
                }

            }
        }

        private void Directorios()
        {
            var subcarpeta = DateTime.Now.ToString("yyyy/MM/dd").Replace(@"/", @"\");
            _directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recepcion", subcarpeta);
            if (!Directory.Exists(_directorio))
                Directory.CreateDirectory(_directorio);
        }
    }
}

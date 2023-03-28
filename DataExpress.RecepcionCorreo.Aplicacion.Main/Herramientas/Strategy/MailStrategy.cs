using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class MailStrategy : ICorreoStrategy
    {
        private readonly MailSettingsDto _settings;
        private List<Message> _lstMessages;
        private List<DocumentosDto> _lstMail = new List<DocumentosDto>();
        private List<DocumentosDto> _lstDocumentos = new List<DocumentosDto>();
        private string _directorio;
        private string _directorioZip;

        public MailStrategy(MailSettingsDto settings) => _settings = settings;

        public List<DocumentosDto> lstMail { get { return _lstMail; } set { _lstMail = value; } }

        public void LeerCorreo()
        {
            Directorios();
            ObtenerMensajes();
            ObtenerZip();
            ExtraerZip();
            ObtenerArchivos();
        }

        private void ObtenerMensajes()
        {
            using (Pop3Client client = new Pop3Client())
            {
                client.Connect(_settings.Host, _settings.Port, _settings.EnableSsl);

                client.Authenticate(_settings.UserName, _settings.Password);

                List<string> uids = client.GetMessageUids();

                _lstMessages = new List<Message>();

                for (int i = 0; i < uids.Count; i++)
                {
                    string currentUidOnServer = uids[i];
                    Message oMessage = client.GetMessage(i + 1);

                    if (oMessage.FindAllAttachments().Count > 0)
                        _lstMessages.Add(oMessage);

                }
                Console.WriteLine($"Numero de correos encontrados: {_lstMessages.Count}");
            }

        }

        private void ObtenerZip()
        {
            var contador = 0;
            foreach (var oMesage in _lstMessages)
            {
                foreach (var attach in oMesage.FindAllAttachments())
                {
                    var _nombre = attach.FileName;
                    Console.WriteLine($"Se encontro el archivo: {_nombre}");
                    if (_nombre.IndexOf("zip") > 0 || _nombre.IndexOf("ZIP") > 0)
                    {
                        
                        var oDocumento = new DocumentosDto
                        {
                            id = contador,
                            nombreDocumento = attach.FileName,
                            documentos = attach.Body
                        };
                        _lstDocumentos.Add(oDocumento);
                    }
                }
                contador++;
            }
        }

        private void ExtraerZip()
        {
            try
            {
                foreach (var zip in _lstDocumentos)
                {
                    var path = $@"{_directorioZip}\{zip.nombreDocumento}";
                    File.WriteAllBytes(path, zip.documentos);
                    ZipFile.ExtractToDirectory(path, _directorio);
                    File.Delete(path);
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
            var subcarpeta = DateTime.Now.ToString("yyyy/MM/").Replace(@"/", @"\");
            _directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recepcion", subcarpeta);
            _directorioZip = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Recepcion\Zip");
            if (!Directory.Exists(_directorio))
                Directory.CreateDirectory(_directorio);

            if (!Directory.Exists(_directorioZip))
                Directory.CreateDirectory(_directorioZip);
        }
    }
}

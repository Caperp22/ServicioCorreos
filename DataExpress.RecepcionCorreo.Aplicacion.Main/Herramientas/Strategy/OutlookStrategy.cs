using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using Microsoft.Office.Interop.Outlook;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using Application = Microsoft.Office.Interop.Outlook.Application;
using Exception = System.Exception;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class OutlookStrategy : ICorreoStrategy
    {
        private Application _outlookApp;
        private NameSpace _outlookNS;
        private MAPIFolder _mails;
        private MAPIFolder _processFolder;
        private Explorer _explorer;
        private Items _correos;
        private ArrayList _emails;
        private string _senderEmailAddress;
        private string _nombre;
        private List<DocumentosDto> _lstMail = new List<DocumentosDto>();
        public string _directorioZip;
        private readonly MailSettingsDto _settings;
        public List<DocumentosDto> lstMail { get { return _lstMail; } set { _lstMail = value; } }

        public OutlookStrategy(MailSettingsDto settings)
        {
            _settings = settings;
            _outlookApp = new Application();
            _outlookNS = _outlookApp.GetNamespace("MAPI");
            //_mails = _outlookNS.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
            //_fldContacts = (MAPIFolder)_outlookApp.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
            //_correos = _outlookApp.ActiveExplorer().CurrentFolder.Items.Restrict("[MessageClass]='IPM.Note'");
            _emails = new ArrayList();
        }


        public void LeerCorreo()
        {
            Directorios();
            try
            {
                if (_settings.ReadAllMail)
                    ObtenerTodosMensajes();
                else
                    ObtenerMensajes();
                Console.WriteLine("FIN: Leer Outlook");

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ERROR " + ex.Message);
           //     Console.ReadKey();
            }
            finally {
              //  Dispose();


            }

        }

        private void ObtenerTodosMensajes()
        {
            //Folder pst.
            _mails = _outlookNS.Stores[_settings.LeerEmail].GetRootFolder();
            //Cuenta Especifica
            //var recip = _outlookApp.Session.CreateRecipient(_settings.LeerEmail);
            //var inboxFolders = _outlookApp.Session.GetSharedDefaultFolder(recip,OlDefaultFolders.olFolderInbox);
            //Leer todas las cuentas
            // var inboxFolders = _outlookNS.Folders;

            DateTime fecha = DateTime.Now;
            if (!string.IsNullOrEmpty(_settings.FechaInicio))
                fecha = DateTime.Parse(_settings.FechaInicio);

            Util.Log("Descargados", $"Asunto,Nombre,Email,Fecha Recibido,archivo");

            foreach (Folder folder in _mails.Folders)
            {
                Console.WriteLine("Folder Base " + folder.Name);

                if (folder.Folders.Count > 0)
                    foreach (MAPIFolder fol in folder.Folders)
                        LeerFolder(fol, fecha);
                else
                    LeerFolder(folder, fecha);

            }
        }
        private void ObtenerMensajes()
        {
            foreach (Items item in _outlookApp.ActiveExplorer().CurrentFolder.Items)
            {
                LeerItems(item,DateTime.Now, true);
            }
        }
        private void LeerFolder(MAPIFolder mapiFolder,DateTime date, bool onlyUnRead = false)
        {
            if (!LeerCarpetaXMes(mapiFolder.Name))
                return;

             if(_settings.ListSkipFolder.Contains(mapiFolder.Name)) 
                return;

            Console.WriteLine("Procesando Folder " + mapiFolder.Name);
            _explorer = _outlookApp.ActiveExplorer();
            var currentFolder = _explorer.CurrentFolder;
            currentFolder = mapiFolder;
            var countItems = currentFolder.Items.Count;

            if (countItems > 0)
                LeerItems(currentFolder.Items, date);

            var countFolders = currentFolder.Folders.Count;
            if (countFolders > 0)
            {
                foreach (Folder fol in currentFolder.Folders)
                {
                    if (!_settings.ListSkipFolder.Contains(fol.Name))
                    {
                        Console.WriteLine("Subfolder " + fol.Name);
                        LeerFolder(fol, date,onlyUnRead);
                    }
                }
            }

        }
        private void LeerItems(Items items, DateTime date, bool onlyUnRead = false)
        {
            var mailItems = items.OfType<MailItem>();
            /// var fechaFin = new DateTime(2022, 11, 30);

            //if (date != DateTime.MinValue)
            //    mailItems = mailItems.Where(p => p.ReceivedTime.Date >= date.Date
            //                                && p.Attachments.Count > 0);
            //mailItems = mailItems.Where(p => p.ReceivedTime.Date >= date.Date
            //                            && p.ReceivedTime.Date <= fechaFin.Date 
            //                            && p.Attachments.Count>0);
            int i = 0;
            //var _processFolder = _mails.Folders["Procesados"];
            //Console.WriteLine("Folder Procesados " + _processFolder.Name);
            foreach (MailItem mail in mailItems)
            {
                try
                {
                    if (mail != null && mail.ReceivedTime.Date >= date.Date)
                        ObtenerAdjuntos(mail, onlyUnRead);
                    else
                        Console.WriteLine("item No procesado");
                    mail.Close(OlInspectorClose.olDiscard);
                    //mail.Move(_processFolder);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exepcion:" + ex.Message);
                }
                finally
                {
                    Console.WriteLine("Correos Procesado " + (i++).ToString());

                }
            }
        }
        private void ObtenerAdjuntos(MailItem mail, bool onlyUnRead = false)
        {
            if (mail != null && mail.To != null)
            {
                Console.WriteLine($"OK el archivo:{mail.Subject}");

                if ((onlyUnRead && mail.UnRead) || !onlyUnRead)
                {
                    _senderEmailAddress = mail.SenderEmailAddress;
                    var date = mail.CreationTime;
                    if (mail.Attachments.Count > 0 && _senderEmailAddress!=null)
                    {
                        Console.WriteLine($"Guardando :{mail.Subject}");

                        var contador = 1;
                        for (int i = 1; i <= mail.Attachments.Count; i++)
                        {
                            _nombre = mail.Attachments[i].FileName;
                            var exist = File.Exists($@"{_directorioZip}\{_nombre}");
                            if (_nombre.ToUpper().IndexOf("ZIP") > 0 && !exist)
                            {
                                var documento = new DocumentosDto
                                {
                                    id = contador,
                                    asunto = mail.Subject,
                                    email = mail.SenderEmailAddress,
                                    recibido = mail.ReceivedTime,
                                    nombreZip = _nombre,
                                    estatus = "DescargaZip"
                                };
                                _lstMail.Add(documento);
                                Console.WriteLine($"Se encontro el archivo: {_nombre} - Recibido: {mail.ReceivedTime.ToString("yyyy-MM-ddTHH:mm:ss")}");
                                mail.Attachments[i].SaveAsFile($@"{_directorioZip}\{_nombre}");
                                string line = $"{mail.Subject},{mail.Sender.Name},{mail.SenderEmailAddress},{mail.ReceivedTime.ToString("yyyy-MM-ddTHH:mm:ss")}," +
                                    $"{_nombre}";
                                Util.Log("Descargados",line);
                            }
                            else
                            {
                                Console.WriteLine($"Ya existe el archivo: {_nombre}");
                                throw new Exception($"Ya existe el archivo: {_nombre}");
                            }
                               
                        }
                    }
                    if (onlyUnRead && mail.UnRead)
                        mail.UnRead = false;
                }
            }
        }
        private void Directorios()
        {
            var subcarpeta = DateTime.Now.ToString("yyyy/MM/").Replace(@"/", @"\");
            _directorioZip = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Recepcion\Zip", subcarpeta);
            var dir = AppDomain.CurrentDomain.BaseDirectory;

            if (!Directory.Exists(_directorioZip))
                Directory.CreateDirectory(_directorioZip);
        }

        private bool LeerCarpetaXMes(string nombreFolder) {

            var hoy = DateTime.Now.ToString("yyyyMM");
            if (!_settings.CarpetaXMes)
                return true;

            if (_settings.CarpetaXMes && nombreFolder.Equals(hoy))
                return true;

            return false;
        }

        private void Dispose()
        {
            if (_outlookApp != null)
            {
                ((IDisposable)_outlookApp).Dispose();
                _outlookApp = null;
            }

            if (_outlookNS != null)
            {
                ((IDisposable)_outlookNS).Dispose();
                _outlookNS = null;
            }
        }
    }
}

using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas
{
    public class EnviarCorreo
    {
        private SmtpConfigurationDTo _smtp;
        private string _sbCuerpo;

        public EnviarCorreo(SmtpConfigurationDTo smtp) => _smtp = smtp;

        public bool EnviarMail(byte[] pdfByte, byte[] xmlByte, string nombrePdf, string nombreXMl, string subject, string receptor, string cuerpomensaje)
        {
            try
            {
                ArmarCorreo(nombreXMl, receptor, cuerpomensaje);
                var subcarpeta = DateTime.Now.ToString("yyyy/MM/").Replace(@"/", @"\");
                var files = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mail", subcarpeta);

                if (!Directory.Exists(files + nombreXMl))
                {
                    Directory.CreateDirectory(files + nombreXMl);
                    using (var message = new MailMessage(_smtp.UserName, _smtp.ReceivingMail))
                    {
                        Stream streamPdf = new MemoryStream(pdfByte);
                        Stream streamXml = new MemoryStream(xmlByte);
                        message.Attachments.Add(new System.Net.Mail.Attachment(streamPdf, nombrePdf));
                        message.Attachments.Add(new System.Net.Mail.Attachment(streamXml, nombreXMl));
                        message.IsBodyHtml = true;
                        message.Subject = subject;
                        message.Body = _sbCuerpo;


                        using (var smtp = new SmtpClient(_smtp.Host, _smtp.Port))
                        {
                            smtp.Credentials = new NetworkCredential(_smtp.UserName, _smtp.Password);
                            smtp.EnableSsl = true;
                            smtp.Send(message);
                        }
                    }
                    Console.WriteLine("Se envío correo de notificación");

                }
                else
                {
                    Console.WriteLine($"El correo del documento {nombreXMl} ya fue enviado");
                }  

                return true;
            }
            catch (Exception ex)
            {
                var message = !string.IsNullOrEmpty(ex.ToString()) ? $"{ex.Message}\r\n{ex.ToString()}" : ex.Message;
                return false;

            }
        }

        private void ArmarCorreo(string xml, string receptor , string cuerpomensaje)
        {
            _sbCuerpo = @"<p><br></p><p>Estimado(a) cliente.</p><p>El xml no pertence al receptor @recept o no contiene la estructura correcta<br></p>
                    <blockquote><ul><li>Comprobante: @TipoDocumento<br></li>
                    <blockquote><ul><li>Detalle: @cuerpomensaje<br></li>
                    </ul></blockquote><p>Saludos cordiales.Servicio proporcionado por:</p><img src='http://dataexpressintmx.com/images/logo.png' style='width: 200px;'/>";
            _sbCuerpo = _sbCuerpo.Replace("@recept", receptor);
            _sbCuerpo = _sbCuerpo.Replace("@TipoDocumento", xml);
            _sbCuerpo = _sbCuerpo.Replace("@cuerpomensaje", cuerpomensaje);
        }
    }
}

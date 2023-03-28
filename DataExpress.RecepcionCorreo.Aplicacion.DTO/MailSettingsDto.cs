using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace DataExpress.RecepcionCorreo.Aplicacion.DTO
{
    public class MailSettingsDto
    {
        public bool OutlookInstalled { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
        public bool ReadAllMail { get; set; }
        public string SkipFolder { get; set; }
        public string LeerEmail { get; set; }
        public string FechaInicio { get; set; }
        public bool CarpetaXMes { get; set; }

        public List<string> ListSkipFolder
        {
            get
            {
                return !string.IsNullOrEmpty(SkipFolder)
                    ? SkipFolder.Split(",").ToList()
                    :new List<string>();

            }
        }
    }

    public class SmtpConfigurationDTo
    {
        public string ReceivingMail { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
    }
}

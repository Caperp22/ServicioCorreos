using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas
{
    public static class Util
    {
        public static void Log(string fileName, string message, bool active = false)
        {
            fileName = "Log";
            active = true;
            if (active)
            {
                var directorioLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Recepcion\Log\");
                var fullFilePath = directorioLog + fileName + ".txt";

                if (!Directory.Exists(directorioLog))
                    Directory.CreateDirectory(directorioLog);
                if (!File.Exists(fullFilePath))
                    File.WriteAllText(fullFilePath, "");
                using (StreamWriter w = File.AppendText(fullFilePath))
                {
                    w.WriteLine(message);
                    w.Flush();
                    w.Close();
                }
            }
        }
    }
}

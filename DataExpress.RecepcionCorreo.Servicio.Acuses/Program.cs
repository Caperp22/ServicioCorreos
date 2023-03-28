using System;
using System.Threading;

namespace DataExpress.RecepcionCorreo.Servicio.Acuses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread tInicio = new Thread(() => { Inicio(); });
            tInicio.IsBackground = true;
            tInicio.Start();
            tInicio.Join();
        }

        static void Inicio()
        {
            while (true)
            {
                Startup.ConfigureServices();
                Startup.acusesAceptacion.InicioAcuse();
                Thread.Sleep(1000);
            }
        }
    }
}

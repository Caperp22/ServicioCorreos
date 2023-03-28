using System.Threading;

namespace DataExpress.RecepcionCorreo.Servicio.XdService
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
            //while (true)
            {
                Startup.ConfigureServices();
                Startup.recepcion.InicioLecturaCorreo();
                Thread.Sleep(1000);
            }
        }
    }
}

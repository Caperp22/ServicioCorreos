using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using DataExpress.RecepcionCorreo.Aplicacion.Main;
using DataExpress.RecepcionCorreo.Servicio.XdService.LogicaNegocio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace DataExpress.RecepcionCorreo.Servicio.XdService
{
    public class Startup
    {
        public static Recepcion recepcion { get; private set; }
        public static IConfiguration configuration { get; private set; }
        public static void ConfigureServices()
        {
            var pathJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            var builder = new ConfigurationBuilder()
                .AddJsonFile(pathJson, true, true)
                .AddEnvironmentVariables();

             configuration = builder.Build();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.Configure<MailSettingsDto>(configuration.GetSection("MailSettings"));
                    services.Configure<SmtpConfigurationDTo>(configuration.GetSection("SmtpConfiguration"));
                    services.Configure<ConfiguracionDto>(configuration.GetSection("Config"));
                    services.AddScoped<ILeerCorreo, LeerCorreo>();
                })
                .UseSerilog()
                .Build();
            recepcion = ActivatorUtilities.CreateInstance<Recepcion>(host.Services);
        }
    }
}

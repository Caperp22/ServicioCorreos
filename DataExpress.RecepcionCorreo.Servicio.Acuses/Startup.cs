using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using DataExpress.RecepcionCorreo.Aplicacion.Main;
using DataExpress.RecepcionCorreo.Infraestructura.Datos;
using DataExpress.RecepcionCorreo.Infraestructura.Interface;
using DataExpress.RecepcionCorreo.Infraestructura.Repository;
using DataExpress.RecepcionCorreo.Servicio.Acuses.LogicaNegocio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace DataExpress.RecepcionCorreo.Servicio.Acuses
{
    public class Startup
    {
        public static AcusesAceptacion acusesAceptacion { get; private set; }

        public static void ConfigureServices()
        {
            var pathJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            var builder = new ConfigurationBuilder()
                .AddJsonFile(pathJson, true, true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.Configure<SmtpConfigurationDTo>(configuration.GetSection("SmtpConfiguration"));
                    services.Configure<ConfiguracionDto>(configuration.GetSection("Config"));
                    services.Configure<AceptantesConfig>(configuration.GetSection("Aceptantes"));
                    services.Configure<MailSettingsDto>(configuration.GetSection("MailSettings"));
                    services.AddScoped<IConnectionFactory, ConnectionFactory>();
                    services.AddScoped<IAcuseRepository, AcuseRepository>();
                    services.AddScoped<IEmisorRepository, EmisorRepository>();
                    services.AddScoped<IAcuse, Acuse>();
                    services.AddScoped<ILeerCorreo, LeerCorreo>();
                })
                .UseSerilog()
                .Build();
            acusesAceptacion = ActivatorUtilities.CreateInstance<AcusesAceptacion>(host.Services);
        }
    }
}

using Carcarbe.Shared.Logging;
using Carcarbe.Shared.Messages;
using Carcarbe.Shared.Repository;
using ConsoleDaemonProducer.Services.Implementation;
using ConsoleDaemonProducer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using System;
using System.Threading.Tasks;

namespace ConsoleDaemonProducer
{
    class Program
    {
        private readonly ILoggerFactory _loggerFactory;
        private static ILogger _logger;
        public static async Task Main(string[] args)
        {

            var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
 
        config.AddEnvironmentVariables();

        if (args != null)
        {
            config.AddCommandLine(args);
        }
    })
    .ConfigureServices((hostContext, services) =>
    {
        var provider = services.BuildServiceProvider();

        var loggerFactory = new LoggerFactory();
        var serviceProvider = services.BuildServiceProvider();
        _logger = serviceProvider.GetService<ILogger<Program>>();
        services.AddOptions();
        services.Configure<DaemonConfig>(hostContext.Configuration.GetSection("Daemon"));
        services.AddScoped<IEnviromentVariableService, EnviromentVariableService>();
        services.AddScoped<IMeasurementService, MeasurementService>();
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddSingleton<IHostedService, DaemonService>();

        services.AddRebus(configure => configure
    .Logging(l => l.Use(new MSLoggerFactoryAdapter(loggerFactory)))
    .Transport(t => t.UseRabbitMqAsOneWayClient("amqp://pklfurgc:4YJosxjltR4AntkkvVignFH-TKW16c9k@raven.rmq.cloudamqp.com/pklfurgc")
    .InputQueueOptions((cfg => { cfg.SetDurable(true); cfg.SetAutoDelete(false, 600000); }))
    
    )
    .Routing(r => r.TypeBased().Map<MeterMessage>("messages-queue"))
    );

    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });
            //_logger.LogInformation("TEST");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            await builder.RunConsoleAsync();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            //Exception ex = (Exception)e.ExceptionObject;
            _logger.LogCritical((e.ExceptionObject as Exception).Message);
        }
    }
}

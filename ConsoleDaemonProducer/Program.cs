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
using Sense.Led;
using Sense.RTIMU;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDaemonProducer
{
    class Program
    {
        private readonly ILoggerFactory _loggerFactory;
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

        services.AddOptions();
        services.Configure<DaemonConfig>(hostContext.Configuration.GetSection("Daemon"));
        services.AddScoped<IEnviromentVariableService, EnviromentVariableService>();
        services.AddScoped<IMeasurementService, MeasurementService>();
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddSingleton<IHostedService, DaemonService>();

        services.AddRebus(configure => configure
    .Logging(l => l.Use(new MSLoggerFactoryAdapter(loggerFactory)))
    .Transport(t => t.UseRabbitMq("amqp://pklfurgc:4YJosxjltR4AntkkvVignFH-TKW16c9k@raven.rmq.cloudamqp.com/pklfurgc", "messages-queue"))
    .Routing(r => r.TypeBased().MapAssemblyOf<MeterMessage>("messages-queue")));
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });


            await builder.RunConsoleAsync();
        }
    }
}

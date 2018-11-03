using Carcarbe.Shared.Logging;
using Carcarbe.Shared.Messages;
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

        services.AddSingleton<IHostedService, DaemonService>();

        //// Register handlers 
        //services.AutoRegisterHandlersFromAssemblyOf<Handler1>();

        // Configure and register Rebus
        //services.AddRebus(configure => configure
        //        .Logging(l => l.Use(new MSLoggerFactoryAdapter(_loggerFactory)))
        //        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672/", "messages-queue"))
        //        .Start());
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

            //        var pixels = Sense.Led.PixelsFromText
            //.Create("A")
            //.SetColor(new Color(255, 255, 255));
            //        Sense.Led.LedMatrix.SetPixels(pixels);
            //        while (true)
            //        {
            //            Sense.Led.LedMatrix.SetLowLight(true);
            //            System.Console.WriteLine("LowLight = true");
            //            System.Console.WriteLine($"Gamma: {string.Join(" ", Sense.Led.LedMatrix.GetGamma().Select(v => v.ToString("X")))}");
            //            Thread.Sleep(2000);

            //            Sense.Led.LedMatrix.SetLowLight(false);
            //            System.Console.WriteLine("LowLight = false");
            //            System.Console.WriteLine($"Gamma: {string.Join(" ", Sense.Led.LedMatrix.GetGamma().Select(v => v.ToString("X")))}");
            //            Thread.Sleep(2000);

            //            var buffer = new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 10, 10 };
            //            Sense.Led.LedMatrix.SetGamma(buffer);
            //            System.Console.WriteLine("Light = custom");
            //            System.Console.WriteLine($"Gamma: {string.Join(" ", Sense.Led.LedMatrix.GetGamma().Select(v => v.ToString("X")))}");
            //            Thread.Sleep(2000);

            //            Sense.Led.LedMatrix.SetLowLight(false);
            //            System.Console.WriteLine("LowLight = false");
            //            System.Console.WriteLine($"Gamma: {string.Join(" ", Sense.Led.LedMatrix.GetGamma().Select(v => v.ToString("X")))}");
            //            Thread.Sleep(2000);
            //        }

            await builder.RunConsoleAsync();
        }
    }
}

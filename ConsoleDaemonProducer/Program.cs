using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        services.AddOptions();
        services.Configure<DaemonConfig>(hostContext.Configuration.GetSection("Daemon"));

        services.AddSingleton<IHostedService, DaemonService>();
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });


            using (var settings = RTIMUSettings.CreateDefault())
            using (var imu = settings.CreateIMU())
            using (var pressure = settings.CreatePressure())
            using (var humidity = settings.CreateHumidity())
            {

                var imuData = imu.GetData();
                Console.WriteLine($"Timestamp: {imuData.Timestamp:O}");
                Console.WriteLine($"FusionPose: Valid: {imuData.FusionPoseValid}, Value: {imuData.FusionPose}");
                Console.WriteLine($"FusionQPose: Valid: {imuData.FusionQPoseValid}, Value: {imuData.FusionQPose}");
                Console.WriteLine($"Gyro: Valid: {imuData.GyroValid}, Value: {imuData.Gyro}");
                Console.WriteLine($"Accel: Valid: {imuData.AccelValid}, Value: {imuData.Accel}");
                Console.WriteLine($"Compass: Valid: {imuData.CompassValid}, Value: {imuData.Compass}");
                Console.WriteLine();

                var pressureReadResult = pressure.Read();
                Console.WriteLine($"Pressure valid: {pressureReadResult.PressureValid}");
                Console.WriteLine($"Pressure: {pressureReadResult.Pressure}");
                Console.WriteLine($"Temperature valid: {pressureReadResult.TemperatureValid}");
                Console.WriteLine($"Temperature: {pressureReadResult.Temperatur}");
                Console.WriteLine();

                var humidityReadResult = humidity.Read();
                Console.WriteLine($"Humidity valid: {humidityReadResult.HumidityValid}");
                Console.WriteLine($"Humidity: {humidityReadResult.Humidity}");
                Console.WriteLine($"Temperature valid: {humidityReadResult.TemperatureValid}");
                Console.WriteLine($"Temperature: {humidityReadResult.Temperatur}");

                Console.WriteLine("===================================================");

            }

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

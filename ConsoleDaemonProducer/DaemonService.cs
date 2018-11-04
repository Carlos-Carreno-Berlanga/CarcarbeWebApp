﻿using Carcarbe.Shared.Messages;
using ConsoleDaemonProducer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rebus.Bus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDaemonProducer
{
    public class DaemonService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<DaemonConfig> _config;
        private readonly IBus _bus;
        private  IEnviromentVariableService _enviromentVariableService;
        private  IMeasurementService _measurementService;
        private readonly IServiceProvider _services;
        public DaemonService(ILogger<DaemonService> logger,
            IOptions<DaemonConfig> config,
            IBus bus,
            IServiceProvider services
            )
        {
            _logger = logger;
            _config = config;
            _bus = bus;
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting daemon: " + _config.Value.DaemonName);
            
            using (var scope = _services.CreateScope())
            {
                _enviromentVariableService = scope.ServiceProvider.GetRequiredService<IEnviromentVariableService>();
                _measurementService = scope.ServiceProvider.GetRequiredService<IMeasurementService>();
                _enviromentVariableService.CreateIfNotExists("COMPUTER_ID", Guid.NewGuid().ToString());
                while (true)
                {
                    _logger.LogInformation($"{DateTime.Now}: Timed Background Service is running:");

                    await DoWorkAsync();
                    await Task.Delay(10000);
                }
            }

        }

        public async Task DoWorkAsync()
        {
            _measurementService.Measure();
            //using (var settings = RTIMUSettings.CreateDefault())
            //using (var imu = settings.CreateIMU())
            //using (var pressure = settings.CreatePressure())
            //using (var humidity = settings.CreateHumidity())
            //{

            //    var imuData = imu.GetData();
            //    Console.WriteLine($"Timestamp: {imuData.Timestamp:O}");
            //    Console.WriteLine($"FusionPose: Valid: {imuData.FusionPoseValid}, Value: {imuData.FusionPose}");
            //    Console.WriteLine($"FusionQPose: Valid: {imuData.FusionQPoseValid}, Value: {imuData.FusionQPose}");
            //    Console.WriteLine($"Gyro: Valid: {imuData.GyroValid}, Value: {imuData.Gyro}");
            //    Console.WriteLine($"Accel: Valid: {imuData.AccelValid}, Value: {imuData.Accel}");
            //    Console.WriteLine($"Compass: Valid: {imuData.CompassValid}, Value: {imuData.Compass}");
            //    Console.WriteLine();

            //    var pressureReadResult = pressure.Read();
            //    Console.WriteLine($"Pressure valid: {pressureReadResult.PressureValid}");
            //    Console.WriteLine($"Pressure: {pressureReadResult.Pressure}");
            //    Console.WriteLine($"Temperature valid: {pressureReadResult.TemperatureValid}");
            //    Console.WriteLine($"Temperature: {pressureReadResult.Temperatur}");
            //    Console.WriteLine();

            //    var humidityReadResult = humidity.Read();
            //    Console.WriteLine($"Humidity valid: {humidityReadResult.HumidityValid}");
            //    Console.WriteLine($"Humidity: {humidityReadResult.Humidity}");
            //    Console.WriteLine($"Temperature valid: {humidityReadResult.TemperatureValid}");
            //    Console.WriteLine($"Temperature: {humidityReadResult.Temperatur}");

            //    Console.WriteLine("===================================================");

            //}
            await _bus.Send(new MeterMessage());
            _logger.LogInformation("===================================================");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping daemon.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing....");

        }
    }
}

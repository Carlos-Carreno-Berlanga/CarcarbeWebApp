using ConsoleDaemonProducer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Sense.RTIMU;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDaemonProducer.Services.Implementation
{
    public class MeasurementService : IMeasurementService
    {
        private readonly ILogger<MeasurementService> _logger;
        public MeasurementService(ILogger<MeasurementService> logger)
        {
            _logger = logger;
        }
        public void Measure()
        {
            try
            {
                using (var settings = RTIMUSettings.CreateDefault())
                using (var pressure = settings.CreatePressure())
                using (var humidity = settings.CreateHumidity())
                {

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




                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

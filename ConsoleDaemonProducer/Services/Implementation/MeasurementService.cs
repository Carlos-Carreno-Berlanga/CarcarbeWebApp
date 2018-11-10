using Carcarbe.Shared.Domain;
using Carcarbe.Shared.Domain.Entities;
using Carcarbe.Shared.Repository;
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
        private readonly IMeasurementRepository _measurementRepository;
        public MeasurementService(ILogger<MeasurementService> logger,
            IMeasurementRepository measurementRepository
            )
        {
            _logger = logger;
            _measurementRepository = measurementRepository;
        }
        public void Measure()
        {
            try
            {
                using (var settings = RTIMUSettings.CreateDefault())
                using (var pressure = settings.CreatePressure())
                using (var humidity = settings.CreateHumidity())
                {

                    RTPressureData pressureReadResult = pressure.Read();
                    Console.WriteLine($"Pressure valid: {pressureReadResult.PressureValid}");
                    Console.WriteLine($"Pressure: {pressureReadResult.Pressure}");
                    Console.WriteLine($"Temperature valid: {pressureReadResult.TemperatureValid}");
                    Console.WriteLine($"Temperature: {pressureReadResult.Temperatur}");
                    Console.WriteLine();

                    RTHumidityData humidityReadResult = humidity.Read();
                    Console.WriteLine($"Humidity valid: {humidityReadResult.HumidityValid}");
                    Console.WriteLine($"Humidity: {humidityReadResult.Humidity}");
                    Console.WriteLine($"Temperature valid: {humidityReadResult.TemperatureValid}");
                    Console.WriteLine($"Temperature: {humidityReadResult.Temperatur}");
                    SenseHatMeasurement senseHatMeasurement = new SenseHatMeasurement(pressureReadResult, humidityReadResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public IEnumerable<Measurement> SplitSenseHatMeasurement(SenseHatMeasurement senseHatMeasurement)
        {

            if (senseHatMeasurement.humidityValueValid)
            {
                yield return new Measurement(senseHatMeasurement.humidityValue, MeasurementType.humidity);
            }

            if (senseHatMeasurement.pressureValueValid)
            {
                yield return new Measurement(senseHatMeasurement.pressureValue, MeasurementType.pressure);
            }

            //if (senseHatMeasurement.humidityTemperatureValueValid)
            //{
            //    yield return new Measurement(senseHatMeasurement.humidityTemperatureValue, MeasurementType.humidity);
            //}

        }

        public void Save(SenseHatMeasurement senseHatMeasurement)
        {

            var measurements = SplitSenseHatMeasurement(senseHatMeasurement);

            foreach(var measurement in measurements)
            {
                _measurementRepository.Add(measurement);
            }
         
        }
    }
}

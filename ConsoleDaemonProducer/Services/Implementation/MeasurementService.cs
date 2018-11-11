using Carcarbe.Shared.Domain;
using Carcarbe.Shared.Domain.Entities;
using Carcarbe.Shared.Repository;
using ConsoleDaemonProducer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Sense.RTIMU;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public SenseHatMeasurement Measure()
        {
            SenseHatMeasurement senseHatMeasurement = null;
            try
            {
                using (var settings = RTIMUSettings.CreateDefault())
                using (var pressure = settings.CreatePressure())
                using (var humidity = settings.CreateHumidity())
                {

                    RTPressureData pressureReadResult = pressure.Read();

                    _logger.LogInformation($"Pressure valid: {pressureReadResult.PressureValid}");
                    _logger.LogInformation($"Pressure: {pressureReadResult.Pressure}");
                    _logger.LogInformation($"Temperature valid: {pressureReadResult.TemperatureValid}");
                    _logger.LogInformation($"Temperature: {pressureReadResult.Temperatur}");


                    RTHumidityData humidityReadResult = humidity.Read();
                    _logger.LogInformation($"Humidity valid: {humidityReadResult.HumidityValid}");
                    _logger.LogInformation($"Humidity: {humidityReadResult.Humidity}");
                    _logger.LogInformation($"Temperature valid: {humidityReadResult.TemperatureValid}");
                    _logger.LogInformation($"Temperature: {humidityReadResult.Temperatur}");
                    senseHatMeasurement = new SenseHatMeasurement(pressureReadResult, humidityReadResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return senseHatMeasurement;
        }

        public IEnumerable<Measurement> SplitSenseHatMeasurement(SenseHatMeasurement senseHatMeasurement)
        {
            if (senseHatMeasurement != null)
            {
                if (senseHatMeasurement.humidityValueValid)
                {
                    yield return new Measurement(senseHatMeasurement.humidityValue, MeasurementType.humidity);
                }

                if (senseHatMeasurement.pressureValueValid)
                {
                    yield return new Measurement(senseHatMeasurement.pressureValue, MeasurementType.pressure);
                }

                if (senseHatMeasurement.humidityTemperatureValueValid)
                {
                    yield return new Measurement(senseHatMeasurement.humidityTemperatureValue, MeasurementType.temperature_humidity);
                }

                if (senseHatMeasurement.pressureTemperatureValueValid)
                {
                    yield return new Measurement(senseHatMeasurement.pressureTemperatureValue, MeasurementType.temperature_pressure);
                }
            }
        }

        public async Task SaveAsync(SenseHatMeasurement senseHatMeasurement)
        {

            var measurements = SplitSenseHatMeasurement(senseHatMeasurement);

            foreach (var measurement in measurements)
            {
                await _measurementRepository.AddAsync(measurement);
            }

        }
    }
}

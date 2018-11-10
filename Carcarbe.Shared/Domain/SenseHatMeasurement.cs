using Sense.RTIMU;
using System;
using System.Collections.Generic;
using System.Text;

namespace Carcarbe.Shared.Domain
{
    public class SenseHatMeasurement
    {
        public SenseHatMeasurement(RTPressureData rTPressureData, RTHumidityData rTHumidityData)
        {
            pressureValue = rTPressureData.Pressure;
            pressureValueValid = rTPressureData.PressureValid;

            pressureTemperatureValue = rTPressureData.Temperatur;
            pressureTemperatureValueValid = rTPressureData.PressureValid;

            humidityValue = rTHumidityData.Humidity;
            humidityTemperatureValue = rTHumidityData.Temperatur;
        }

        public float pressureValue { get; set; }
        public bool pressureValueValid { get; set; }

        public float pressureTemperatureValue { get; set; }
        public bool pressureTemperatureValueValid { get; set; }

        public float humidityValue { get; set; }
        public bool humidityValueValid { get; set; }

        public float humidityTemperatureValue { get; set; }
        public bool humidityTemperatureValueValid { get; set; }
    }
}

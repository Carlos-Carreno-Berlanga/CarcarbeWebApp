using Sense.RTIMU;

namespace ConsoleDaemonProducer.Domain
{
    public class Measurement
    {
        public Measurement(RTPressureData rTPressureData, RTHumidityData rTHumidityData)
        {
            pressureValue = rTPressureData.Pressure;
            pressureTemperatureValue = rTPressureData.Temperatur;

            humidityValue = rTHumidityData.Humidity;
            humidityTemperatureValue = rTHumidityData.Temperatur;
        }
        public float pressureValue { get; set; }
        public float pressureTemperatureValue { get; set; }

        public float humidityValue { get; set; }
        public float humidityTemperatureValue { get; set; }
    }
}

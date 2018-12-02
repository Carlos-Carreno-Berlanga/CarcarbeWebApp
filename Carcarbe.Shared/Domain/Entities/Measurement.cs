using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Carcarbe.Shared.Domain.Entities
{
    public class Measurement : BaseEntity
    {

        [JsonConstructor]
        public Measurement(float value, MeasurementType measurementType)
        {
            Value = value;
            MeasurementType = measurementType;
        }

        public Measurement(int id, decimal value, string measurement_type, DateTime time_stamp_timezone)
        {
            Id = id;
            Value =(float) value;
            MeasurementType =(MeasurementType) Enum.Parse(typeof(MeasurementType), measurement_type);
            TimeStamp = time_stamp_timezone;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public float Value { get; set; }

        [Required]
        public MeasurementType MeasurementType { get; set; }

        //public string MeasurementType { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}

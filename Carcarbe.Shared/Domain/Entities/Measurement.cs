using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Carcarbe.Shared.Domain.Entities
{
    public class Measurement : BaseEntity
    {
        public Measurement(float value, MeasurementType measurementType)
        {
            Value = value;
            MeasurementType = measurementType;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public float Value { get; set; }

        [Required]
        public MeasurementType MeasurementType { get; set; }

    }
}

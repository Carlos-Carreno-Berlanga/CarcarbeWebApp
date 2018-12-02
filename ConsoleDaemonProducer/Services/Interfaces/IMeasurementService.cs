using Carcarbe.Shared.Domain;
using Carcarbe.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDaemonProducer.Services.Interfaces
{
    public interface IMeasurementService
    {
        SenseHatMeasurement Measure();

        Task SaveAsync(SenseHatMeasurement measurement);

        IEnumerable<Measurement> SplitSenseHatMeasurement(SenseHatMeasurement senseHatMeasurement);
    }
}

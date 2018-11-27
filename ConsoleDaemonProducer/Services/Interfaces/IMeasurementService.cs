using Carcarbe.Shared.Domain;
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
    }
}

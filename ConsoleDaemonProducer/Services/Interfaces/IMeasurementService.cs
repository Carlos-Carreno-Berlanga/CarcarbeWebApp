using Carcarbe.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDaemonProducer.Services.Interfaces
{
    public interface IMeasurementService
    {
        void Measure();

        void Save(SenseHatMeasurement measurement);
    }
}

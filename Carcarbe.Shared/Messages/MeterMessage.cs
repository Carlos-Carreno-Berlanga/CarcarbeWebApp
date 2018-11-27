using System;

namespace Carcarbe.Shared.Messages
{
    public class MeterMessage
    {
        public MeterMessage()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public override string ToString()
        {
            return $"Message1 : {Id}";
        }
    }
}

using System;

namespace Carcarbe.Shared.Messages
{
    public class MeterMessage
    {
        public MeterMessage(string data)
        {
            Id = Guid.NewGuid();
            Data = data;
        }

        public Guid Id { get; }
        public String Data { get; }

        public override string ToString()
        {
            return $"Message1 : {Id}";
        }
    }
}

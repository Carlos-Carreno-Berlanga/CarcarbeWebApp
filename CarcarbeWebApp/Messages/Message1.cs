﻿using System;

namespace CarcarbeWebApp.Messages
{
    public class Message1
    {
        public Message1()
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

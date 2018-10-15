using CarcarbeWebApp.Messages;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Handlers
{
    public class Handler1 : IHandleMessages<Message1>
    {
        private readonly ILogger _logger;

        public Handler1(ILogger<Handler1> logger)
        {
            _logger = logger;
        }

        public Task Handle(Message1 message)
        {
            _logger.LogInformation("Handler1 received : {message}", message);

            return Task.CompletedTask;
        }   
    }
}

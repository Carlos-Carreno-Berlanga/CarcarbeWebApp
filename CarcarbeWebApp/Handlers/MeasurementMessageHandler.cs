using Carcarbe.Shared.Domain.Entities;
using Carcarbe.Shared.Messages;
using CarcarbeWebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Handlers
{
    public class MeasurementMessageHandler : IHandleMessages<MeterMessage>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<Notifier> _hubContext;
        public MeasurementMessageHandler(ILogger<MeterMessage> logger,
            IHubContext<Notifier> hubContext
            )
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public Task Handle(MeterMessage message)
        {
            _logger.LogInformation("MeasurementMessageHandler received : {message}", message);
            var measurements = JsonConvert.DeserializeObject<IEnumerable<Measurement>>(message.Data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor });
            foreach(var measurement in measurements)
            {
                measurement.TimeStamp = DateTime.UtcNow;
            }

            _hubContext.Clients.All.SendAsync("Notify", measurements);
            return Task.CompletedTask;
        }
    }
}

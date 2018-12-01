using Carcarbe.Shared.Messages;
using CarcarbeWebApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
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
            _hubContext.Clients.All.SendAsync("Notify", message);
            return Task.CompletedTask;
        }
    }
}

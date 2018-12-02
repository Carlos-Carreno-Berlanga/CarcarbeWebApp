using Carcarbe.Shared.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Hubs
{
    public class Notifier : Hub
    {
        public Task Notify(IEnumerable<Measurement> measurements)
        {
            return Clients.All.SendAsync("Notify", measurements);
        }
    }
}

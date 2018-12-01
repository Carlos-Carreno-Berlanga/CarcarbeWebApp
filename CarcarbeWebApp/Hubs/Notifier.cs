using Carcarbe.Shared.Messages;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CarcarbeWebApp.Hubs
{
    public class Notifier : Hub
    {
        public Task Notify(MeterMessage message)
        {
            return Clients.All.SendAsync("Notify", message);
        }
    }
}

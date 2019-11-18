using BatchStats.Models;
using Microsoft.AspNetCore.SignalR;

namespace BatchStats.Api.Hubs
{
    public class CalcDataHub : Hub
    {
        public void PushUpdate(CalcData calcData)
        {
            Clients.All.SendAsync("BatchUpdate", calcData);
        }
    }
}   
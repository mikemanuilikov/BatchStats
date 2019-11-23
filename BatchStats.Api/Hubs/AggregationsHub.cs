using BatchStats.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BatchStats.Api.Hubs
{
    public interface IAggregationsHub 
    {
        Task PushBatchStats(CalcData calcData);
    }

    public class AggregationsHub : Hub<IAggregationsHub>
    {
    }
}
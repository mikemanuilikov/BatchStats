using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BatchStats.Core.Queries
{
    public class GetCaclDataQuery : IQuery<CalcData[]>
    {
        public DateTimeOffset? StartTime { get; set; }
    }

    public class GetCalcDataQueryHandler : IQueryHandler<GetCaclDataQuery, CalcData[]>
    {
        public Task<CalcData[]> HandleAsync(GetCaclDataQuery query)
        {
            var rand = new Random();
            long minTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(100)).ToUnixTimeSeconds();

            var calcData = Enumerable
                            .Range(1, 1000000)
                            .Select(x => new CalcData
                            {
                                BatchId = x.ToString(),
                                SensorId = rand.Next(1, 5).ToString(),
                                Average = rand.Next(20, 40).ToString(),
                                StandardDeviation = rand.Next(0, 5).ToString(),
                                BatchStartTime = minTime + rand.Next(1000, 1000000000)
                            })
                            .ToArray();

            return Task.FromResult(calcData);
        }
    }
}

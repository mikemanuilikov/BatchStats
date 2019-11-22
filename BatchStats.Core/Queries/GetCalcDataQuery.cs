using BatchStats.Core.Entities;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Options;
using BatchStats.Models;
using MongoDB.Driver;
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
        private readonly IDbSettings dbSettings;
        private readonly IMongoDatabase db;

        public GetCalcDataQueryHandler(IDbSettings dbSettings, IMongoDatabase db)
        {
            this.dbSettings = dbSettings;
            this.db = db;
        }

        public async Task<CalcData[]> HandleAsync(GetCaclDataQuery query)
        {
            var month = TimeSpan.FromDays(30);
            long defaultStartTime = DateTimeOffset.UtcNow.Subtract(month).ToUnixTimeSeconds();
            long startTime = query.StartTime?.ToUnixTimeSeconds() ?? defaultStartTime;

            var results = await db.GetCollection<Aggregation>(dbSettings.AggregationsCollectionName)
                            .Find(x => x.BatchStartTime >= startTime)
                            .ToListAsync();

            return results
                    .Select(x => new CalcData
                    {
                        BatchId = x.BatchId,
                        SensorId = x.SensorId,
                        Average = x.Average,
                        StandardDeviation = x.StandardDeviation,
                        BatchStartTime = x.BatchStartTime
                    })
                    .ToArray();
        }
    }
}
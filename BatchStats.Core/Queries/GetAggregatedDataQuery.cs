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
    public class GetAggregatedDataQuery : IQuery<CalcData[]>
    {
        public string SensorId { get; set; }

        public int Limit { get; set; }
    }

    public class GetCalcDataQueryHandler : IQueryHandler<GetAggregatedDataQuery, CalcData[]>
    {
        private readonly IDbSettings dbSettings;
        private readonly IMongoDatabase db;

        public GetCalcDataQueryHandler(IDbSettings dbSettings, IMongoDatabase db)
        {
            this.dbSettings = dbSettings;
            this.db = db;
        }

        public async Task<CalcData[]> HandleAsync(GetAggregatedDataQuery query)
        {
            var results = await db.GetCollection<Aggregation>(dbSettings.AggregationsCollectionName)
                            .Find(x => x.SensorId.ToLower() == query.SensorId.ToLower())
                            .SortBy(x => x.BatchStartTime)
                            .Limit(query.Limit)
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
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
    public class GetRawDataQuery : IQuery<DataPoint[]>
    {
        public DateTimeOffset StartTime { get; set; }

        public string SensorId { get; set; }
    }

    public class GetRawDataQueryHandler : IQueryHandler<GetRawDataQuery, DataPoint[]>
    {
        private readonly IDbSettings dbSettings;
        private readonly IMongoDatabase db;

        public GetRawDataQueryHandler(IDbSettings dbSettings, IMongoDatabase db)
        {
            this.dbSettings = dbSettings;
            this.db = db;
        }

        public async Task<DataPoint[]> HandleAsync(GetRawDataQuery query)
        {
            long startTime = query.StartTime.ToUnixTimeSeconds();

            var results = await db.GetCollection<RawTelemetry>(dbSettings.RawTelemetryCollectionName)
                            .Find(x => x.BatchTimestamp >= startTime && x.SensorId == query.SensorId)
                            .ToListAsync();

            return results
                    .Select(x => new DataPoint
                    {
                        BatchId = x.BatchId,
                        SensorId = x.SensorId,
                        Value = x.Value,
                        BatchTimestamp = x.BatchTimestamp
                    })
                    .ToArray();
        }
    }
}
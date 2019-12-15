using BatchStats.Core.Entities;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Options;
using BatchStats.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace BatchStats.Core.Queries
{
    public class GetRawDataQuery : IQuery<DataPoint[]>
    {
        public int Limit { get; set; }
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
            var results = await db.GetCollection<RawTelemetry>(dbSettings.RawTelemetryCollectionName)
                            .Find(x => x.Id != null) // dummy check, api lacks "FindAll" query 
                            .SortBy(x => x.BatchTimestamp)
                            .Limit(query.Limit)
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
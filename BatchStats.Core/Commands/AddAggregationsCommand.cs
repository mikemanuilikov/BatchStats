using BatchStats.Core.Entities;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Options;
using BatchStats.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BatchStats.Core.Commands
{
    public class AddAggregationsCommand : ICommand
    {
        public CalcData CalcData { get; set; }
    }

    public class AddAggregationsCommandHandler : ICommandHandler<AddAggregationsCommand>
    {
        private readonly IMongoDatabase database;
        private readonly IDbSettings dbSettings;

        public AddAggregationsCommandHandler(IMongoDatabase database, IDbSettings dbSettings)
        {
            this.database = database;
            this.dbSettings = dbSettings;
        }

        public async Task Handle(AddAggregationsCommand command)
        {
            var collection = database.GetCollection<Aggregation>(dbSettings.AggregationsCollectionName);

            await collection.InsertOneAsync(new Aggregation
            {
                BatchId = command.CalcData.BatchId,
                SensorId = command.CalcData.SensorId,
                BatchStartTime = command.CalcData.BatchStartTime,
                Average = command.CalcData.Average,
                StandardDeviation = command.CalcData.StandardDeviation
            });
        }
    }
}
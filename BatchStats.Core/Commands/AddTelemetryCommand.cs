using BatchStats.Core.Entities;
using BatchStats.Core.Interfaces;
using BatchStats.Core.Options;
using BatchStats.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BatchStats.Core.Commands
{
    public class AddTelemetryCommand : ICommand
    {
        public DataPoint SensorData { get; set; }
    }

    public class AddTelemetryCommandHandler : ICommandHandler<AddTelemetryCommand>
    {
        private readonly IMongoDatabase database;
        private readonly IDbSettings dbSettings;

        public AddTelemetryCommandHandler(IMongoDatabase database, IDbSettings dbSettings)
        {
            this.database = database;
            this.dbSettings = dbSettings;
        }

        public async Task Handle(AddTelemetryCommand command)
        {
            var collection = database.GetCollection<RawTelemetry>(dbSettings.RawTelemetryCollectionName);

            await collection.InsertOneAsync(new RawTelemetry 
            {
                BatchId = command.SensorData.BatchId,
                SensorId = command.SensorData.SensorId,
                BatchTimestamp = command.SensorData.BatchTimestamp,
                Value = command.SensorData.Value
            });
        }
    }
}
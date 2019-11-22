using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BatchStats.Core.Entities
{
    public class RawTelemetry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string BatchId { get; set; }

        public string SensorId { get; set; }

        public string Value { get; set; }

        [BsonRepresentation(BsonType.Int64)]
        public long BatchTimestamp { get; set; }
    }
}
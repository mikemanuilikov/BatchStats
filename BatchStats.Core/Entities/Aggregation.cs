using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BatchStats.Core.Entities
{
    public class Aggregation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string BatchId { get; set; }

        [BsonRepresentation(BsonType.Int64)]
        public long BatchStartTime { get; set; }

        public string SensorId { get; set; }

        public string Average { get; set; }

        public string StandardDeviation { get; set; }
    }
}
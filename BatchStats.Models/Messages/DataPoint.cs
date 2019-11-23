namespace BatchStats.Models
{
    public class DataPoint : IMessage
    {
        public string BatchId { get; set; }

        public string SensorId { get; set; }

        public decimal Value { get; set; }

        public long BatchTimestamp { get; set; }
    }
}
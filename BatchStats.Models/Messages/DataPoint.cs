namespace BatchStats.Models
{
    public class DataPoint : IMessage
    {
        public string BatchId { get; set; }

        public string SensorId { get; set; }

        public string Value { get; set; }

        public long BatchStartTime { get; set; }
    }
}
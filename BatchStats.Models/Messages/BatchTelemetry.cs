using System.Collections.Generic;

namespace BatchStats.Models
{
    public class BatchTelemetry : IMessage
    {
        public string BatchId { get; set; }

        public long BatchStartTime { get; set; }

        public List<DataPoint> DataPoints { get; set; }
    }
}
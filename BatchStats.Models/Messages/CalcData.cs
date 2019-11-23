using System;

namespace BatchStats.Models
{
    public class CalcData : IMessage
    {
        public string BatchId { get; set; }

        public long BatchStartTime { get; set; }

        public string SensorId { get; set; }

        public decimal Average { get; set; }

        public decimal StandardDeviation { get; set; }
    }
}
using System;

namespace BatchStats.Models
{
    public class CalcData : IMessage
    {
        public string BatchId { get; set; }

        public long BatchStartTime { get; set; }

        public string SensorId { get; set; }

        public string Average { get; set; }

        public string StandardDeviation { get; set; }
    }
}
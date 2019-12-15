namespace BatchStats.Core.Options
{
    public class BatchSettings : IBatchSettings
    {
        public long BatchCachingTime { get; set; }

        public long MaxCycleTime { get; set; }

        public int AggregationsPageLimit { get; set; }

        public int RawDataPageLimit { get; set; }
    }

    public interface IBatchSettings
    {
        long BatchCachingTime { get; set; }

        long MaxCycleTime { get; set; }

        int AggregationsPageLimit { get; set; }

        int RawDataPageLimit { get; set; }
    }
}

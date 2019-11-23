namespace BatchStats.Core.Options
{
    public class BatchSettings : IBatchSettings
    {
        public long BatchCachingTime { get; set; }

        public long MaxCycleTime { get; set; }
    }

    public interface IBatchSettings
    {
        long BatchCachingTime { get; set; }

        long MaxCycleTime { get; set; }
    }
}

using BatchStats.Models;

namespace BatchStats.Core.Interfaces
{
    public interface IBatchCachingService
    {
        void AddCurrentBatchTelemetry(BatchTelemetry batchTelemetry);

        BatchTelemetry GetCurrentBatchTelemetry();
    }
}
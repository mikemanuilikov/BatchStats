using BatchStats.Core.Interfaces;
using BatchStats.Core.Options;
using BatchStats.Models;
using System;

namespace BatchStats.Core.Services
{
    public class BatchCachingService : IBatchCachingService
    {
        private readonly ICacheAccessor cache;
        private readonly IBatchSettings batchSettings;
        private const string BatchCacheKey = "CurrentBatchTelemetry";

        public BatchCachingService(ICacheAccessor cache, IBatchSettings batchSettings)
        {
            this.cache = cache;
            this.batchSettings = batchSettings;
        }

        public void AddCurrentBatchTelemetry(BatchTelemetry batchTelemetry)
        {
            cache.Store(BatchCacheKey, batchTelemetry, TimeSpan.FromSeconds(batchSettings.BatchCachingTime));
        }

        public BatchTelemetry GetCurrentBatchTelemetry()
        {
            if (cache.TryGet(BatchCacheKey, out BatchTelemetry batchTelemetry))
            {
                return batchTelemetry;
            }

            return null;
        }
    }
}
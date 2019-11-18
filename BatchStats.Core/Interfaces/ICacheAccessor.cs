using System;

namespace BatchStats.Core.Interfaces
{
    public interface ICacheAccessor
    {
        void Store<TValue>(string key, TValue value, TimeSpan expiration);

        bool TryGet<TValue>(string key, out TValue value);
    }
}

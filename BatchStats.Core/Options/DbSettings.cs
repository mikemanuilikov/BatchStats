namespace BatchStats.Core.Options
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string RawTelemetryCollectionName { get; set; }
        public string AggregationsCollectionName { get; set; }
    }

    public interface IDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string RawTelemetryCollectionName { get; set; }
        string AggregationsCollectionName { get; set; }
    }
}
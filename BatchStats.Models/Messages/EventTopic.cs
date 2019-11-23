namespace BatchStats.Models
{
    public enum EventTopic : short
    {
        Default = 0,
        RawData = 1,
        BatchData = 2,
        CalcData = 4
    }
}
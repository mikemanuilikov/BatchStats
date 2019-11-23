using BatchStats.Core.Interfaces;
using BatchStats.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BatchStats.Core.Services
{
    public class CalcService : ICalcService
    {
        public Task<CalcData[]> Calculate(BatchTelemetry batchTelemetry)
        {
            var batchStatsBySensors = batchTelemetry.DataPoints.GroupBy(x => x.SensorId)
                .Select(x =>
                {
                    decimal avg = 0, stdDev = 0;

                    try
                    {
                        avg = x.Average(y => y.Value);
                        stdDev = (decimal)Math.Sqrt((double)x.Select(y => y.Value - avg).Select(y => y * y).Average());
                    }
                    catch (OverflowException)
                    {
                        //todo: logs
                    }

                    return new CalcData
                    {
                        BatchId = batchTelemetry.BatchId,
                        BatchStartTime = batchTelemetry.BatchStartTime,
                        SensorId = x.Key,
                        Average = avg,
                        StandardDeviation = stdDev
                    };
                })
                .ToArray();

            return Task.FromResult(batchStatsBySensors);
        }
    }
}

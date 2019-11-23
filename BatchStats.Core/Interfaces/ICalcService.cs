using BatchStats.Models;
using System.Threading.Tasks;

namespace BatchStats.Core.Interfaces
{
    public interface ICalcService
    {
        Task<CalcData[]> Calculate(BatchTelemetry batchTelemetry);
    }
}

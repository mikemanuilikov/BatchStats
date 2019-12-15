using BatchStats.Core.Interfaces;
using BatchStats.Core.Options;
using BatchStats.Core.Queries;
using BatchStats.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BatchStats.Api.Controllers
{
    public class WebAppController : Controller
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly IBatchSettings batchSettings;

        public WebAppController(IQueryDispatcher queryDispatcher, IBatchSettings batchSettings)
        {
            this.queryDispatcher = queryDispatcher;
            this.batchSettings = batchSettings;
        }

        [HttpGet("calc-data/{sensorId?}")]
        [AllowAnonymous]
        public async Task<CalcData[]> GetCalcData([FromRoute]string sensorId = "temperature")
        {
            var query = new GetAggregatedDataQuery 
            {
                SensorId = sensorId,
                Limit = batchSettings.AggregationsPageLimit
            };

            return await queryDispatcher.ExecuteAsync(query);
        }

        [HttpGet("raw-data")]
        [AllowAnonymous]
        public async Task<DataPoint[]> GetRawData()
        {
            var query = new GetRawDataQuery
            {
                Limit = batchSettings.RawDataPageLimit
            };

            return await queryDispatcher.ExecuteAsync(query);
        }
    }
}
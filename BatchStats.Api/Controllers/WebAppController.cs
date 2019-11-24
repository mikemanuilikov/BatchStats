using BatchStats.Core.Interfaces;
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

        public WebAppController(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet("calc-data")]
        [AllowAnonymous]
        public async Task<CalcData[]> GetCalcData()
        {
            var month = TimeSpan.FromDays(30);
            var defaultStartTime = DateTimeOffset.UtcNow.Subtract(month);

            var query = new GetCaclDataQuery 
            {
                StartTime = defaultStartTime,
                SensorId = "Temperature"
            };

            return await queryDispatcher.ExecuteAsync(query);
        }

        [HttpGet("raw-data")]
        [AllowAnonymous]
        public async Task<DataPoint[]> GetRawData()
        {
            var week = TimeSpan.FromDays(7);
            var defaultStartTime = DateTimeOffset.UtcNow.Subtract(week);

            var query = new GetRawDataQuery
            {
                StartTime = defaultStartTime
            };

            return await queryDispatcher.ExecuteAsync(query);
        }
    }
}
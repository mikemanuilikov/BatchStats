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
        public async Task<CalcData[]> GetCalcData(DateTimeOffset? startTime = null)
        {
            var query = new GetCaclDataQuery 
            {
                StartTime = startTime
            };

            return await queryDispatcher.ExecuteAsync(query);
        }
    }
}

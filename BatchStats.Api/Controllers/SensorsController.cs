using BatchStats.Core.Interfaces;
using BatchStats.Models;
using Microsoft.AspNetCore.Mvc;

namespace BatchStats.Api.Controllers
{
    public class SensorsController : Controller
    {
        private readonly IEventBus eventBus;

        public SensorsController(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        [HttpPost("sensors/telemetry")]
        public IActionResult AddTelemetry([FromBody]DataPoint dataPoint)
        {
            eventBus.Publish(EventTopic.RawData, dataPoint);

            return Ok("ok");
        }
    }
}
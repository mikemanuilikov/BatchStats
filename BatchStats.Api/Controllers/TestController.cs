using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatchStats.Api.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult GetState()
        {
            return Ok("running");
        }
    }
}

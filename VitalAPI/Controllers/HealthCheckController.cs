using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VitalAPI.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous] 
        public IActionResult HealthCheck()
        {
            return Ok(new { status = "Healthy" });
        }

    }
}

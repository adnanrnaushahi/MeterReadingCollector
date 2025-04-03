using Microsoft.AspNetCore.Mvc;
using System.Net;
using Asp.Versioning;

namespace MeterReading.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    public class MeterReadingController : ControllerBase
    {
        [HttpPost("meter-reading-uploads")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IActionResult))]
        public IActionResult UploadMeterReading()
        {
            return Ok();
        }
    }
}

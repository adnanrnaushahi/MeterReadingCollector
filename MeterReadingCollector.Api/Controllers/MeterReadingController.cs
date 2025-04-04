using System.Net;
using Asp.Versioning;
using MeterReadingCollector.Business.Models;
using MeterReadingCollector.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeterReadingCollector.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpPost("meter-reading-uploads")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MeterReadingResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IActionResult))]
        public async Task<IActionResult> UploadMeterReading(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Uploaded file is empty");
            }

            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Should be csv a file");
            }

            var response = await _meterReadingService.ProcessCsvFileAsync(file);

            return Ok(response);
        }
    }
}

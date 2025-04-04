using MeterReadingCollector.Business.Models;
using Microsoft.AspNetCore.Http;

namespace MeterReadingCollector.Business.Services
{
    public interface IMeterReadingService
    {
        Task<MeterReadingResponse> ProcessCsvFileAsync(IFormFile file);
    }
}

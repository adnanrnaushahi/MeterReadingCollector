
using MeterReadingCollector.Common;

namespace MeterReadingCollector.Api.Extensions;

public static class Application
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
    }
}

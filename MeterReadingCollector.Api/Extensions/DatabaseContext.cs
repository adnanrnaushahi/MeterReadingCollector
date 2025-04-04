using MeterReadingCollector.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingCollector.Api.Extensions;

public static class DatabaseContext
{
	public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)  {
        
        services.AddDbContext<MeterReadingContext>(options =>
        {
            options.UseInMemoryDatabase("MeterReadingDb");
        });


    }
}

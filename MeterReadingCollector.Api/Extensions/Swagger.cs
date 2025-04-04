

using MeterReadingCollector.Api.Swagger;

namespace MeterReadingCollector.Api.Extensions;

public static class Swagger
{
	public static void AddSwagger(this IServiceCollection services)
	{
		services.ConfigureOptions<ConfigureSwaggerOptions>();

		services.AddSwaggerGen(options =>
		{
			options.OperationFilter<SwaggerDefaultValues>();
		});
	}
}

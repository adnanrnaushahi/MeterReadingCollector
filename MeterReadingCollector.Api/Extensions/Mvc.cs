using Asp.Versioning;

namespace MeterReadingCollector.Api.Extensions;

public static class Mvc
{
	public static void AddServiceMvc(this IServiceCollection services)
	{
		services.AddApiVersioning()
				.AddMvc()
				.AddApiExplorer(
					options =>
					{
						// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
						// note: the specified format code will format the version as "'v'major[.minor][-status]"
						options.GroupNameFormat = "'v'VVV";

						// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
						// can also be used to control the format of the API version in route templates
						options.SubstituteApiVersionInUrl = true;
						options.DefaultApiVersion = ApiVersion.Default;
					});

		services.Configure<RequestLocalizationOptions>(options =>
		{
			options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-GB");
		});

		services
			.AddControllers()
			.ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
			.AddNewtonsoftJson(
				options =>
				{
					options.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-GB");
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
					options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
				});

		services.AddApiVersioning(options =>
		{
			// reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
			options.ReportApiVersions = true;
		});
	}
}

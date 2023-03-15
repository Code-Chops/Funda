using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api.Configuration;
using Fundalyzer.Infrastructure.Api.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fundalyzer.Infrastructure.Api;

public static class ApiInfrastructureRegistrationExtensions
{
	public static IServiceCollection AddApiInfrastructureLayer(this IServiceCollection services, IConfigurationSection configRoot)
	{
		if (services is null) throw new ArgumentNullException(nameof(services));
		if (configRoot is null) throw new ArgumentNullException(nameof(configRoot));
		
		services
			.AddOptions<Settings>(Settings.SectionName)
			.Bind(configRoot)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddSingleton<Settings>();
		services.AddSingleton<ILogger, Logger<FundaClient>>();
		
		services.AddSingleton<IEstateSupplyRepo, FundaClient>(serviceProvider =>
		{
			var settings = serviceProvider.GetRequiredService<IOptions<Settings>>();
			var logger = serviceProvider.GetRequiredService<Logger<FundaClient>>();
			
			var restClient = new RestClient(c =>
			{
				c.BaseUrl = new($"{settings.Value.ApiUrl}/{settings.Value.ApiKey}");
				c.ThrowOnAnyError = true;
			});
			
			var httpClient = new FundaHttpClient(restClient, new RetryRateLimitingStrategy());
			return new FundaClient(timer: new PeriodicTimer(TimeSpan.FromSeconds(1)), httpClient, settings, logger);
		});

		return services;
	}
}
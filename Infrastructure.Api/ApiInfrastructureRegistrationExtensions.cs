using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api.Configuration;
using Fundalyzer.Infrastructure.Api.HttpClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fundalyzer.Infrastructure.Api;

public static class ApiInfrastructureRegistrationExtensions
{
	public static IServiceCollection AddApiInfrastructureLayer(this IServiceCollection services, Settings settings)
	{
		if (services is null) 
			throw new ArgumentNullException(nameof(services));

		services.AddSingleton(settings);
		services.AddSingleton<ILogger, Logger<FundaClient>>();

		services.AddSingleton<IFundaHttpClient, FundaHttpClient>(_ =>
		{
			var restClient = new RestClient(c =>
			{
				c.BaseUrl = new($"{settings.ApiUrl}/{settings.ApiKey}");
			});

			return new FundaHttpClient(restClient, new RetryRateLimitingStrategy());
		});
		
		services.AddSingleton<IEstateSupplyRepo, FundaClient>(serviceProvider =>
		{
			var requestDelayInMs = TimeSpan.FromMilliseconds(settings.RequestDelayInMilliseconds);
			
			var fundaHttpClient = serviceProvider.GetRequiredService<IFundaHttpClient>();
			var logger = serviceProvider.GetRequiredService<ILogger<FundaClient>>();
			return new FundaClient(timer: new PeriodicTimer(requestDelayInMs), fundaHttpClient, settings, logger);
		});

		return services;
	}
}
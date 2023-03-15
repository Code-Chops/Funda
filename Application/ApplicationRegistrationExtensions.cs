using System.Globalization;
using Fundalyzer.Application.BackgroundTasks;
using Fundalyzer.Domain;
using Fundalyzer.Infrastructure.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fundalyzer.Application;

public static class ApplicationRegistrationExtensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services, Settings settings)
	{
		// Register services.
		services.AddSingleton<IAgencyApplicationService, AgencyApplicationService>();
		services.AddSingleton<ILogger, Logger<AgencyApplicationService>>();
		
		// Use the invariant culture throughout the application.
		CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

		// Add a background service.
		services.AddHostedService<AgencyRankingBackgroundService>();

		services.AddMemoryCache();
		
		services.AddDomainLayer();
		services.AddApiInfrastructureLayer(settings);
		
		return services;
	}
}
using System.Globalization;
using Fundalyzer.Application.BackgroundTasks;
using Fundalyzer.Domain;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fundalyzer.Application;

public static class ApplicationRegistrationExtensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfigurationSection configRoot)
	{
		// Register services.
		services.AddSingleton<IApplicationService, ApplicationService>();
		services.AddSingleton<ILogger, Logger<ApplicationService>>();
		
		// Use the invariant culture throughout the application.
		CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

		// Add two background services.
		services.AddHostedService<AgencyRankingService>(serviceProvider => new AgencyRankingService(
			agencyRanker: new AgenciesHavingMostEstatesWithGardenInAms(serviceProvider.GetRequiredService<IEstateSupplyRepo>()),
			logger: serviceProvider.GetRequiredService<ILogger<AgencyRankingService>>()));

		services.AddHostedService<AgencyRankingService>(serviceProvider => new AgencyRankingService(
			agencyRanker: new AgenciesHavingMostEstatesWithoutGardenInAms(serviceProvider.GetRequiredService<IEstateSupplyRepo>()),
			logger: serviceProvider.GetRequiredService<ILogger<AgencyRankingService>>()));

		services.AddDomainLayer();
		services.AddApiInfrastructureLayer(configRoot);
		
		return services;
	}
}
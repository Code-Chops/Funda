using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundalyzer.Application.BackgroundTasks;

/// <summary>
/// A background service which retrieves real estate supply and orders / filters it based on an <see cref="IAgencyRanker"/>.
/// </summary>
public sealed class AgencyRankingBackgroundService : BackgroundService
{
	private IMemoryCache MemoryCache { get; }
	private IEstateSupplyRepo SupplyRepo { get; }
	private ILogger<AgencyRankingBackgroundService> Logger { get; }
	
	public AgencyRankingBackgroundService(IMemoryCache memoryCache, IEstateSupplyRepo supplyRepo, ILogger<AgencyRankingBackgroundService> logger)
	{
		this.MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
		this.SupplyRepo = supplyRepo ?? throw new ArgumentNullException(nameof(supplyRepo));
		this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		var agenciesHavingMostEstatesWithoutGardenInAms = new AgenciesHavingMostEstatesInAmsterdam(this.SupplyRepo);
		await this.Synchronize(agenciesHavingMostEstatesWithoutGardenInAms, cancellationToken);

		var agenciesHavingMostEstatesWithGardenInAms = new AgenciesHavingMostEstatesWithGardenInAmsterdam(this.SupplyRepo);
		await this.Synchronize(agenciesHavingMostEstatesWithGardenInAms, cancellationToken);
	}

	// Wrapped because it makes it easier to unit test (so the task won't be executed in the background).
	internal async Task Synchronize(IAgencyRanker agencyRanker, CancellationToken cancellationToken)
	{
		var rankingName = agencyRanker.GetType().Name;
		
		this.Logger.Log(LogLevel.Information, "{job}: Starting background job for {ranker}.", nameof(AgencyRankingBackgroundService), rankingName);

		var rankedAgencies = await agencyRanker.ListAndRankAsync(cancellationToken);
		this.MemoryCache.Set(rankingName, rankedAgencies);
		
		this.Logger.Log(LogLevel.Information, "{job}: Ended background job for {ranker}.", nameof(AgencyRankingBackgroundService), rankingName);
	}
}
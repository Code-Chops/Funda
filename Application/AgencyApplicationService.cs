using Fundalyzer.Application.Adapters.V1;
using Fundalyzer.Application.BackgroundTasks;
using Fundalyzer.Contracts.V1;
using Fundalyzer.Domain.Agencies.Ranked;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Microsoft.Extensions.Caching.Memory;

namespace Fundalyzer.Application;

/// <summary>
/// Retrieves ranked data from a memory cache. The data will be inserted by <see cref="AgencyRankingBackgroundService"/>.
/// </summary>
public sealed class AgencyApplicationService : IAgencyApplicationService
{
	private IMemoryCache MemoryCache { get; }
	private AgencyRankToContractAdapter AgencyRankToContractAdapter { get; } = new();
	
	public AgencyApplicationService(IMemoryCache memoryCache)
	{
		this.MemoryCache = memoryCache;
	}

	public TopAgenciesResponse? GetAgenciesHavingMostEstatesWithGardenInAmsterdam()
	{
		var result = this.MemoryCache.Get<RankedAgenciesResult>(nameof(AgenciesHavingMostEstatesWithGardenInAmsterdam));

		return result is null 
			? null 
			: this.AgencyRankToContractAdapter.ConvertToContract(result);
	}
	
	public TopAgenciesResponse? GetAgenciesHavingMostEstatesInAmsterdam()
	{
		var result = this.MemoryCache.Get<RankedAgenciesResult>(nameof(AgenciesHavingMostEstatesInAmsterdam));
		
		return result is null 
			? null 
			: this.AgencyRankToContractAdapter.ConvertToContract(result);
	}
}
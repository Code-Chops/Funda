using Fundalyzer.Application.Adapters;
using Fundalyzer.Contracts.V1;
using Fundalyzer.Domain.Agencies.Ranked;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Microsoft.Extensions.Caching.Memory;

namespace Fundalyzer.Application;

public class AgencyApplicationService : IAgencyApplicationService
{
	private IMemoryCache MemoryCache { get; }
	private AgencyRankAdapter AgencyRankAdapter { get; } = new();
	
	public AgencyApplicationService(IMemoryCache memoryCache)
	{
		this.MemoryCache = memoryCache;
	}

	public TopAgenciesResponse? GetAgenciesHavingMostEstatesWithGardenInAmsterdam()
	{
		var result = this.MemoryCache.Get<RankedAgenciesResult>(nameof(AgenciesHavingMostEstatesWithGardenInAmsterdam));

		return result is null 
			? null 
			: this.AgencyRankAdapter.ConvertToContract(result);
	}
	
	public TopAgenciesResponse? GetAgenciesHavingMostEstatesInAmsterdam()
	{
		var result = this.MemoryCache.Get<RankedAgenciesResult>(nameof(AgenciesHavingMostEstatesInAmsterdam));
		
		return result is null 
			? null 
			: this.AgencyRankAdapter.ConvertToContract(result);
	}
}
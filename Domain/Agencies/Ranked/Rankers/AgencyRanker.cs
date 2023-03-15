using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public abstract record AgencyRanker<TSelf> : IAgencyRanker 
	where TSelf : AgencyRanker<TSelf>, IComparer<Agency>, IHasAgencyRankerLimit
{
	public abstract EstateCity City { get; }
	public abstract EstateHasGarden EstateHasGarden { get; }

	private IEstateSupplyRepo EstateSupplyRepo { get; }

	public async Task<RankedAgenciesResult> ListAndRankAsync(CancellationToken cancellationToken)
	{
		var orderedAgencies = (await this.EstateSupplyRepo.GetRealEstateAgenciesAsync(this, cancellationToken))
			.OrderByDescending(agency => agency, (IComparer<Agency>)this)
			.Select((agency, index) => new RankedAgency(rank: index + 1, agency))
			.OrderBy(rankedAgency => rankedAgency.Rank)
			.Take(TSelf.RankingLength)
			.ToList();

		return new RankedAgenciesResult(orderedAgencies);
	}
	
	protected AgencyRanker(IEstateSupplyRepo estateSupplyRepo)
	{
		this.EstateSupplyRepo = estateSupplyRepo;
	}
}
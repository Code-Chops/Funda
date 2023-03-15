using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public abstract record AgenciesWithMostEstates<TSelf> : AgencyRanker<TSelf>, IComparer<Agency>
	where TSelf : AgenciesWithMostEstates<TSelf>, IHasAgencyRankerLimit
{
	protected AgenciesWithMostEstates(IEstateSupplyRepo estateSupplyRepo) 
		: base(estateSupplyRepo)
	{
	}
	
	int IComparer<Agency>.Compare(Agency? x, Agency? y)
	{
		var countX = x?.Estates.Count ?? 0;
		var countY = y?.Estates.Count ?? 0;
		
		if (countX < countY)
			return -1;

		if (countX > countY)
			return 1;

		return 0;
	}
}

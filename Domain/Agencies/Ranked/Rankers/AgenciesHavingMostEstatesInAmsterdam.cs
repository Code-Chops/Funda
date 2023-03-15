using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public sealed record AgenciesHavingMostEstatesInAmsterdam : AgenciesWithMostEstates<AgenciesHavingMostEstatesInAmsterdam>, IHasAgencyRankerLimit
{
	public static int RankingLength => 10;
	
	public override EstateCity City { get; } = new("Amsterdam");
	public override EstateFacilities? Facilities => null;

	public AgenciesHavingMostEstatesInAmsterdam(IEstateSupplyRepo estateSupplyRepo) 
		: base(estateSupplyRepo)
	{
	}
}
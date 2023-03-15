using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public sealed record AgenciesHavingMostEstatesWithGardenInAmsterdam : AgenciesWithMostEstates<AgenciesHavingMostEstatesWithGardenInAmsterdam>, IHasAgencyRankerLimit
{
	public static int RankingLength => 10;

	public override EstateCity City { get; } = new("Amsterdam");
	public override EstateFacilities Facilities => EstateFacilities.Garden;

	public AgenciesHavingMostEstatesWithGardenInAmsterdam(IEstateSupplyRepo estateSupplyRepo) 
		: base(estateSupplyRepo)
	{
	}
}
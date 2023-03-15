using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public sealed record AgenciesHavingMostEstatesWithoutGardenInAms : AgenciesWithMostEstates<AgenciesHavingMostEstatesWithoutGardenInAms>, IHasAgencyRankerLimit
{
	public static int RankingLength => 10;
	
	public override EstateCity City { get; } = new("Amsterdam");
	public override EstateHasGarden EstateHasGarden { get; } = new(false);

	public AgenciesHavingMostEstatesWithoutGardenInAms(IEstateSupplyRepo estateSupplyRepo) 
		: base(estateSupplyRepo)
	{
	}
}
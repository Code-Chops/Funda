using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public sealed record AgenciesHavingMostEstatesWithGardenInAms : AgenciesWithMostEstates<AgenciesHavingMostEstatesWithGardenInAms>, IHasAgencyRankerLimit
{
	public static int RankingLength => 10;

	public override EstateCity City { get; } = new("Amsterdam");
	public override EstateHasGarden EstateHasGarden { get; } = new(true);

	public AgenciesHavingMostEstatesWithGardenInAms(IEstateSupplyRepo estateSupplyRepo) 
		: base(estateSupplyRepo)
	{
	}
}
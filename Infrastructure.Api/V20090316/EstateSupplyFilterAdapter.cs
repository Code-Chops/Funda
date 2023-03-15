using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api.V20090316.Objects;

namespace Fundalyzer.Infrastructure.Api.V20090316;

public sealed record EstateSupplyFilterAdapter 
{
	public EstateSupplyFilter ConvertToSupplyFilter(IAgencyRanker ranker)
	{
		var facilities = ranker.EstateHasGarden
			? Faciliteiten.Tuin
			: (Faciliteiten?)null;

		return new(supplyType: SoortAanbod.Koop, ranker.City, facilities);
	}
}
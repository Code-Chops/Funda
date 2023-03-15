using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api.V20090316.Objects;

namespace Fundalyzer.Infrastructure.Api.V20090316;

public sealed record EstateSupplyFilterAdapter 
{
	public EstateSupplyFilter ConvertToSupplyFilter(IAgencyRanker ranker)
	{
		Faciliteiten? facilities = null;

		if (ranker.Facilities is not null)
		{
			if (ranker.Facilities.HasFlag(EstateFacilities.Garden))
				facilities = Faciliteiten.Tuin;
					
			if (ranker.Facilities.HasFlag(EstateFacilities.SwimmingPool))
				facilities = Faciliteiten.Zwembad;
		}

		return new(supplyType: SoortAanbod.Koop, ranker.City, facilities);
	}
}
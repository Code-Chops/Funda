using Fundalyzer.Contracts.V1;
using Fundalyzer.Domain.Agencies.Ranked;
using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Application.Adapters;

public class AgencyRankAdapter
{
	public TopAgenciesResponse ConvertToContract(RankedAgenciesResult result)
	{
		return new TopAgenciesResponse()
		{
			Agencies = result.Agencies.Select(a => new RankedAgencyContract()
			{
				EstateCount = a.Agency.Estates.Count,
				Rank = a.Rank,
				Name = a.Agency.Name,
			}).ToList(),
			EstateCity = result.City,
			HasGarden = result.EstateFacilities?.HasFlag(EstateFacilities.Garden)
		};
	}
}
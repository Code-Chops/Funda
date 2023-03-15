using Fundalyzer.Contracts.V1;
using Fundalyzer.Domain.Agencies.Ranked;
using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Application.Adapters.V1;

/// <summary>
/// Converts the domain objects to a contract.
/// </summary>
public sealed class AgencyRankToContractAdapter
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
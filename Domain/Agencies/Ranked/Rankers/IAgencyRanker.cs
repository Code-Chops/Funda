using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

public interface IAgencyRanker : IDomainService
{
	EstateCity City { get; }
	EstateFacilities? Facilities { get; }

	Task<RankedAgenciesResult> ListAndRankAsync(CancellationToken cancellationToken);
}
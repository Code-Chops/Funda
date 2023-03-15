using Fundalyzer.Domain.Agencies;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;

namespace Fundalyzer.Domain.Estates;

public interface IEstateSupplyRepo
{
	Task<IEnumerable<Agency>> GetRealEstateAgenciesAsync(IAgencyRanker ranker, CancellationToken cancellationToken = default);
}
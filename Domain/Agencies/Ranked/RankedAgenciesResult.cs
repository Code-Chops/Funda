using System.Collections.Immutable;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked;

/// <summary>
/// Agencies ranked by <see cref="Ranker"/>.
/// </summary>
public sealed record RankedAgenciesResult
{
	public EstateCity City { get; }
	public EstateFacilities? EstateFacilities { get; }

	public ImmutableList<RankedAgency> Agencies { get; }

	public IAgencyRanker Ranker { get; }
	
	public RankedAgenciesResult(EstateCity city, EstateFacilities? facilities, IEnumerable<RankedAgency> agencies, IAgencyRanker ranker)
	{
		this.City = city;
		this.EstateFacilities = facilities;
		this.Agencies = agencies.ToImmutableList();
		this.Ranker = ranker;
	}
}
using System.Collections.Immutable;
using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked;

public sealed record RankedAgenciesResult
{
	public EstateCity City { get; }
	public EstateFacilities? EstateFacilities { get; }

	public ImmutableList<RankedAgency> Agencies { get; }

	public RankedAgenciesResult(EstateCity city, EstateFacilities? facilities, IEnumerable<RankedAgency> agencies)
	{
		this.City = city;
		this.EstateFacilities = facilities;
		this.Agencies = agencies.ToImmutableList();
	}
}
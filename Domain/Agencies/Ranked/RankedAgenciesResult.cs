using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies.Ranked;

[GenerateIdentity<(EstateCity, EstateHasGarden)>(nameof(TopAgenciesId))]
public class RankedAgenciesResult : ListEntity<RankedAgenciesResult, TopAgenciesId, RankedAgency>
{
	public EstateCity City => this.Id.Value.Item1;
	public EstateHasGarden EstateHasGarden => this.Id.Value.Item2;

	public IReadOnlyCollection<RankedAgency> Agencies => this.List;
	protected override IReadOnlyList<RankedAgency> List { get; }

	public RankedAgenciesResult(IReadOnlyList<RankedAgency> list)
	{
		this.List = list;
	}
}
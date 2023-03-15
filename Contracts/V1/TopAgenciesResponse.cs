namespace Fundalyzer.Contracts.V1;

public sealed record TopAgenciesResponse
{
	public required string EstateCity { get; init; }
	public required bool? HasGarden { get; init; }
	public required List<RankedAgencyContract> Agencies { get; init; }
}
namespace Fundalyzer.Contracts.FundalyzerApi;

public sealed record TopAgenciesResponse
{
	public required string EstateCity { get; init; }
	public required List<RankedAgency> Agencies { get; init; }
}
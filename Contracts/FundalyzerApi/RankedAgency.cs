namespace Fundalyzer.Contracts.FundalyzerApi;

public sealed record RankedAgency : Contract
{
	public required int Rank { get; init; }
	public required string Name { get; init; }
	public required int EstateCount { get; init; }
}
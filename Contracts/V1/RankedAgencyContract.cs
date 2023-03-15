namespace Fundalyzer.Contracts.V1;

public sealed record RankedAgencyContract : Contract
{
	public required int Rank { get; init; }
	public required string Name { get; init; }
	public required int EstateCount { get; init; }
}
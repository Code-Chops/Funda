namespace Fundalyzer.Contracts.FundalyzerApi;

public sealed record TopAgenciesRequest : Contract
{
	public bool HasGarden { get; init; }
}
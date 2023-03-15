namespace Fundalyzer.Domain.Agencies.Ranked;

/// <summary>
/// A ranked <see cref="Agency"/>.
/// </summary>
public readonly record struct RankedAgency : IValueObject
{
	public int Rank { get; }
	public Agency Agency { get; }

	public RankedAgency(int rank, Agency agency)
	{
		this.Rank = rank;
		this.Agency = agency;
	}
}
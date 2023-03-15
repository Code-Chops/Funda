namespace Fundalyzer.Domain.Estates;

[GenerateIdentity<long>(nameof(EstateId))]
public sealed class Estate : Entity<EstateId>
{
	public EstateCity City { get; }
	public EstateHasGarden HasGarden { get; }

	public Estate(EstateCity city, EstateHasGarden hasGarden)
	{
		this.City = city;
		this.HasGarden = hasGarden;
	}
}
namespace Fundalyzer.Domain.Estates;

[GenerateIdentity<long>(nameof(EstateId))]
public sealed class Estate : Entity<EstateId>
{
	public EstateCity City { get; }
	public EstateFacilities? Facilities { get; }

	public Estate(EstateCity city, EstateFacilities? facilities)
	{
		this.City = city;
		this.Facilities = facilities;
	}
}
namespace Fundalyzer.Domain.Agencies;

[GenerateIdentity<long>(nameof(AgencyId))]
public sealed class Agency : Entity<AgencyId>
{
	public override string ToString() => $"{nameof(Agency)} {this.Name} ({nameof(this.Id)}: {this.Id}), {nameof(this.Estates)}: {this.Estates.Count}";
	
	public AgencyName Name { get; }
	public AgencyEstates Estates { get; }

	public Agency(AgencyName name, AgencyEstates estates)
	{
		this.Name = name;
		this.Estates = estates;
	}
}
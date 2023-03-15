using Fundalyzer.Infrastructure.Api.V20090316.Objects;

namespace Fundalyzer.Infrastructure.Api.V20090316;

public sealed record EstateSupplyFilter
{
	public override string ToString() => this.Name;
	
	public string? SupplyType { get; } 
	public string? QueryString { get; } 
	
	public string Name { get; }
	
	public EstateSupplyFilter(SoortAanbod supplyType, string? city, Faciliteiten? facilities)
	{
		if (supplyType != SoortAanbod.None)
			this.SupplyType = supplyType.ToString().ToLowerInvariant();
		
		if (city is not null) 
			this.QueryString += $"/{city.ToLowerInvariant()}";

		var facilityList = Enum.GetValues<Faciliteiten>()
			.Where(facility => facilities?.HasFlag(facility) ?? false)
			.Select(facility => facility.ToString().ToLowerInvariant())
			.ToList();
		
		if (facilityList.Any())
			this.QueryString += $"/{String.Join("/", facilityList)}";

		var optionalCity = city is not null ? $" in {city}" : null;
		var optionalFacilities = facilityList.Any() 
			? $" with {String.Join(" & ", facilityList)}"
			: null;
		
		this.Name = $"estate supply{optionalCity}{optionalFacilities}";
	}
}
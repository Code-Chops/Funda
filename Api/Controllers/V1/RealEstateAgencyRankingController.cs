using Fundalyzer.Application;

namespace Fundalyzer.Api.Controllers.V1;

/// <summary>
/// Exposes 2 endpoints which both perform a filter / ranking on the estates of an agency.
/// </summary>
// This API is complete not RESTful or according to any standard. It is just a way to retrieve data from the application. 
//
// If another version will be released this version can be placed in the Api\Controllers\V1 folder and the new one in Api\V2, etc.
[ApiController]
[Route("")]
public sealed class RealEstateAgencyRankingController : ControllerBase
{
	private IAgencyApplicationService ApplicationService { get; }
	
	public RealEstateAgencyRankingController(IAgencyApplicationService agencyApplicationService)
	{
		this.ApplicationService = agencyApplicationService ?? throw new ArgumentNullException(nameof(agencyApplicationService));
	}

	/// <summary>
	/// Gets the top 10 of agencies having most estates with a garden in Amsterdam. Returns a 425 (Too Early) when service has not finished retrieving yet.
	/// </summary>
	[HttpGet("AgenciesHavingMostEstatesWithGardenInAmsterdam")]
	public IActionResult GetAgenciesHavingMostEstatesWithGardenInAmsterdam()
	{
		var result = this.ApplicationService.GetAgenciesHavingMostEstatesWithGardenInAmsterdam();
		
		return result is null 
			? this.StatusCode(425)  // HTTP status code: too early
			: this.Ok(result);
	}
	
	/// <summary>
	/// Gets the top 10 of agencies having most estates in Amsterdam. Returns a 425 (Too Early) when service has not finished retrieving yet.
	/// </summary>
	[HttpGet("AgenciesHavingMostEstatesInAmsterdam")]
	public IActionResult GetAgenciesHavingMostEstatesInAmsterdam()
	{
		var result = this.ApplicationService.GetAgenciesHavingMostEstatesInAmsterdam();
		
		return result is null 
			? this.StatusCode(425)  // HTTP status code: too early
			: this.Ok(result);
	}
}
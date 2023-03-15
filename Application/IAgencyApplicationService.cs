using Fundalyzer.Contracts.V1;

namespace Fundalyzer.Application;

public interface IAgencyApplicationService : IApplicationService
{
	TopAgenciesResponse? GetAgenciesHavingMostEstatesWithGardenInAmsterdam();
	TopAgenciesResponse? GetAgenciesHavingMostEstatesInAmsterdam();
}
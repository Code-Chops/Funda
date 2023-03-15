using Microsoft.Extensions.DependencyInjection;

namespace Fundalyzer.Domain;

public static class DomainRegistrationExtensions
{
	public static IServiceCollection AddDomainLayer(this IServiceCollection services)
	{
		// Nothing to do.
		
		return services;
	}
}
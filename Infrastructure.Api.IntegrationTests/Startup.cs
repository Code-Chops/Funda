using Fundalyzer.Infrastructure.Api.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Fundalyzer.Infrastructure.Api.IntegrationTests;

public class Startup
{
	private static readonly string ApiInfrastructurePath = typeof(ApiInfrastructureRegistrationExtensions).Assembly.Location;
	
	public void ConfigureServices(IServiceCollection services)
	{
		var configRoot = new ConfigurationBuilder().AddJsonFile(ApiInfrastructurePath, optional: false).Build();

		var accessor = new TestOutputHelperAccessor();
		services.AddLogging(a => a.AddProvider(new XunitTestOutputLoggerProvider(accessor)));

		services.AddApiInfrastructureLayer(configRoot.GetRequiredSection(Settings.SectionName));
	}
}
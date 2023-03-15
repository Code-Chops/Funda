using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace Fundalyzer.Infrastructure.Api.IntegrationTests;

public sealed class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		var settings = new Settings()
		{
			ApiUrl = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json",
			ApiKey = "ac1b0b1572524640a0ecc54de453ea9f",
			PageSize = 25,
			RequestDelayInMilliseconds = 60
		};
		
		services.AddApiInfrastructureLayer(settings);
	}
	
	public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
		loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
}
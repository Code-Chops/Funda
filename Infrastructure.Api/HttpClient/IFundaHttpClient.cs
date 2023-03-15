using Fundalyzer.Infrastructure.Api.V20090316;

namespace Fundalyzer.Infrastructure.Api.HttpClient;

public interface IFundaHttpClient
{
	Task<Main?> GetPageAsync(RestRequest request, CancellationToken cancellationToken);
}
using System.Net;
using Fundalyzer.Infrastructure.Api.Configuration;
using Fundalyzer.Infrastructure.Api.Exceptions;
using Fundalyzer.Infrastructure.Api.V20090316;

namespace Fundalyzer.Infrastructure.Api.HttpClient;

/// <summary>
/// Wraps a REST HTTP-client and contains a rate limit strategy which is executed when rate limiting occurs <see cref="IRateLimitingStrategy"/>/ 
/// </summary>
internal sealed class FundaHttpClient : IFundaHttpClient
{
	private IRestClient RestClient { get; }
	private IRateLimitingStrategy RateLimitingStrategy { get; }

	public FundaHttpClient(IRestClient restClient, IRateLimitingStrategy rateLimitingStrategy)
	{
		this.RestClient = restClient;
		this.RateLimitingStrategy = rateLimitingStrategy;
	}

	public async Task<Main?> GetPageAsync(RestRequest request, CancellationToken cancellationToken)
	{
		var httpResponse = await this.RateLimitingStrategy.ExecuteAsync(() => this.RestClient.ExecuteAsync<Main>(request, cancellationToken));

		return httpResponse.StatusCode switch
		{
			HttpStatusCode.OK												=> httpResponse.Data,
			HttpStatusCode.TooManyRequests or HttpStatusCode.Unauthorized	=> throw new TooManyRequestsException(),
			HttpStatusCode.NotFound											=> default,
			_																=> throw new UnexpectedResponseStatusException(
				message: $"Unexpected error code {httpResponse.StatusCode.ToString()} was returned by Funda when requesting {nameof(Main)}.", 
				statusCode: httpResponse.StatusCode)
		};
	}
}
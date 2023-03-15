using System.Net;
using Fundalyzer.Infrastructure.Api.Configuration;
using Fundalyzer.Infrastructure.Api.Exceptions;

namespace Fundalyzer.Infrastructure.Api.HttpClient;

/// <summary>
/// Wraps an HTTP-client and contains a rate limit strategy which is executed when rate limiting occurs <see cref="IRateLimitingStrategy"/>/ 
/// </summary>
internal class FundaHttpClient : IFundaHttpClient
{
	private IRestClient RestClient { get; }
	private IRateLimitingStrategy RateLimitingStrategy { get; }

	public FundaHttpClient(IRestClient restClient, IRateLimitingStrategy rateLimitingStrategy)
	{
		this.RestClient = restClient;
		this.RateLimitingStrategy = rateLimitingStrategy;
	}

	public async Task<IEnumerable<TResponse>> GetListAsync<TResponse>(RestRequest request, CancellationToken cancellationToken)
	{
		return await this.GetAsync<IEnumerable<TResponse>>(request, cancellationToken) 
		       ?? Array.Empty<TResponse>();
	}

	public async Task<TResponse?> GetSingleAsync<TResponse>(RestRequest request, CancellationToken cancellationToken)
	{
		return await this.GetAsync<TResponse>(request, cancellationToken) 
		       ?? default;
	}

	public async Task<TResponse?> GetAsync<TResponse>(RestRequest request, CancellationToken cancellationToken)
	{
		var httpResponse = await this.RateLimitingStrategy.ExecuteAsync(() => this.RestClient.ExecuteAsync(request, cancellationToken));

		return httpResponse.StatusCode switch
		{
			HttpStatusCode.OK				=> ((RestResponse<TResponse>)httpResponse).Data,
			HttpStatusCode.TooManyRequests	=> throw new TooManyRequestsException(),
			HttpStatusCode.NotFound			=> default,
			_								=> throw new UnexpectedResponseStatusException(
												message: $"Unexpected error code {httpResponse.StatusCode.ToString()} was returned by Funda when requesting {typeof(TResponse).Name}.", 
												statusCode: httpResponse.StatusCode)
		};
	}
}
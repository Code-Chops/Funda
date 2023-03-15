using System.Net;
using Polly;

namespace Fundalyzer.Infrastructure.Api.Configuration;

/// <summary>
/// Will retry the request multiple times (with a delay), if a rate limit is encountered.
/// </summary>
public sealed class RetryRateLimitingStrategy : IRateLimitingStrategy
{
    private const int DefaultRetries = 5;
    private static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromSeconds(3);
    
    private AsyncPolicy<RestResponse> Policy { get; }

    /// <summary>
    /// Creates an instance with reasonable default values for the rate limits described by the API.
    /// </summary>
    public RetryRateLimitingStrategy()
        : this(DefaultRetries, DefaultRetryInterval)
    {
    }

    /// <summary>
    /// Creates an instance that uses the provided configuration values for retrying.
    /// </summary>
    public RetryRateLimitingStrategy(int retries, TimeSpan retryInterval)
    {
        this.Policy = Polly.Policy
            .HandleResult<RestResponse>(response => response.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(retries, _ => retryInterval);
    }

    /// <inheritdoc />
    public Task<RestResponse> ExecuteAsync(Func<Task<RestResponse>> action)
    {
        return this.Policy.ExecuteAsync(action);
    }
}
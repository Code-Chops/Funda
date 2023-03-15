using System.Net;

namespace Fundalyzer.Infrastructure.Api.Exceptions;

/// <summary>
/// Will be thrown when too many requests are being send to Funda.
/// </summary>
public sealed class TooManyRequestsException : HttpRequestException
{
    public TooManyRequestsException(string? message = null, Exception? innerException = null)
        : base(
            message: message ?? $"Too many requests to the Funda API: {HttpStatusCode.TooManyRequests}. This should not happen.", 
            inner: innerException, 
            statusCode: HttpStatusCode.TooManyRequests)
    {
    }
}
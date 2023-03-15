using System.Net;
using Fundalyzer.Infrastructure.Api.HttpClient;

namespace Fundalyzer.Infrastructure.Api.Exceptions;

/// <summary>
/// Is thrown when an other HTTP status code is returned by the Funda API then the expected ones at <see cref="FundaHttpClient.GetPageAsync"/>
/// </summary>
public sealed class UnexpectedResponseStatusException : HttpRequestException
{
    public UnexpectedResponseStatusException(HttpStatusCode statusCode)
        : this($"Received unexpected response status: {statusCode}.", statusCode)
    {
    }

    public UnexpectedResponseStatusException(string message, HttpStatusCode statusCode, Exception? innerException = null)
        : base(message, innerException, statusCode)
    {
    }
}
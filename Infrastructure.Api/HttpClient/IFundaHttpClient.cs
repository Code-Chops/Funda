namespace Fundalyzer.Infrastructure.Api.HttpClient;

public interface IFundaHttpClient
{
	Task<IEnumerable<TResponse>> GetListAsync<TResponse>(RestRequest request, CancellationToken cancellationToken);
	Task<TResponse?> GetSingleAsync<TResponse>(RestRequest request, CancellationToken cancellationToken);
}
using System.Collections.Immutable;
using Fundalyzer.Domain.Agencies;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api.HttpClient;
using Fundalyzer.Infrastructure.Api.V20090316;
using Microsoft.Extensions.Logging;

namespace Fundalyzer.Infrastructure.Api;

/// <summary>
/// A client which pulls real estate supply from the Funda API. It takes the following steps:
/// <list type="number">
/// <item>It start from page 1: The pages in Funda are 1-based (the page size is configured in appsettings).</item>
/// <item>It calls the FundaHttpClient to retrieve the current page.</item>
/// <item>If the page is the last page it has finished the job and it quits, otherwise it takes the next step.</item>
/// <item>It increments the current page and repeats from step 2.</item>
/// </list> 
/// </summary>
// Memory caching could be added here as well. In order to cache the responses of Funda, based on the complete URL (including query string).
public sealed class FundaClient : IEstateSupplyRepo
{
	/// <summary>
	/// Timer with an interval of each request to Funda.
	/// </summary>
	private PeriodicTimer Timer { get; }

	private IFundaHttpClient HttpClient { get; }
	private Settings Settings { get; }
	private ILogger<FundaClient> Logger { get; }
	private EstateSupplyFilterAdapter Adapter { get; }
	
	public FundaClient(PeriodicTimer timer, IFundaHttpClient httpClient, Settings settings, ILogger<FundaClient> logger)
	{
		this.Timer = timer ?? throw new ArgumentNullException(nameof(timer));
		this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
		this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		this.Adapter = new EstateSupplyFilterAdapter();
	}

	public async Task<IEnumerable<Agency>> GetRealEstateAgenciesAsync(IAgencyRanker ranker, CancellationToken cancellationToken = default)
	{
		var filter = this.Adapter.ConvertToSupplyFilter(ranker);
		
		var currentPage = 1;
		this.Logger.Log(LogLevel.Information, "{client}: Starting retrieving {filter} at page {page}.", nameof(FundaClient), filter.Name, currentPage);

		var pages = new List<Main>();
		while (await this.Timer.WaitForNextTickAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
		{
			var page = await this.GetRealEstateFromPageAsync(filter, currentPage, cancellationToken);

			pages.Add(page);
			
			this.Logger.Log(LogLevel.Information, "Stored {count} estates from page {page}.", page.Objects.Count, currentPage);

			// Finished with syncing.
			if (currentPage == page.Paging.AantalPaginas)
				break;
			
			currentPage++;
		}
		
		this.Logger.Log(LogLevel.Information, "{client}: Finished!", nameof(FundaClient));
		
		return pages
			.SelectMany(p => p.Objects)
			.GroupBy(o => (Id: o.MakelaarId, Name: o.MakelaarNaam))
			.Select(group => new Agency(
				name: new(group.Key.Name),
				estates: new AgencyEstates(value: group.ToList().Select(_ => new Estate(ranker.City, ranker.Facilities)).ToImmutableList()))
				{ Id = new(group.Key.Id) });
	}

	private async Task<Main> GetRealEstateFromPageAsync(EstateSupplyFilter filter, int pageNumber, CancellationToken cancellationToken)
	{
		this.Logger.Log(LogLevel.Information, "Retrieving estates from Funda, page: {page}.", pageNumber);

		var pageSize = this.Settings.PageSize;

		var request = new RestRequest()
		{
			Method = Method.Get,
		};
		
		if (filter.SupplyType is not null)
			request = request.AddParameter(EstateSupplyQueryParameter.Type, filter.SupplyType, ParameterType.QueryString);

		if (filter.QueryString is not null)
			request.AddParameter(EstateSupplyQueryParameter.Query, filter.QueryString, ParameterType.QueryString, encode: false);
			
		// Add paging
		request = request
			.AddParameter(EstateSupplyQueryParameter.Page, pageNumber, ParameterType.QueryString)
			.AddParameter(EstateSupplyQueryParameter.PageSize, pageSize, ParameterType.QueryString);

		var page = await this.HttpClient.GetPageAsync(request, cancellationToken)
		           ?? throw new InvalidDataException($"Received estates from Funda page {pageNumber} (with size {pageSize}) for {filter.Name} is null.");
		
		return page;
	}
}
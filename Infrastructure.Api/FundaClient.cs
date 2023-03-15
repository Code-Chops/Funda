using System.Collections.Immutable;
using Fundalyzer.Domain.Agencies;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Fundalyzer.Infrastructure.Api.Configuration;
using Fundalyzer.Infrastructure.Api.HttpClient;
using Fundalyzer.Infrastructure.Api.V20090316;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
	private IOptions<Settings> Settings { get; }
	private ILogger<FundaClient> Logger { get; }
	private EstateSupplyFilterAdapter Adapter { get; }
	
	public FundaClient(PeriodicTimer timer, IFundaHttpClient httpClient, IOptions<Settings> settings, ILogger<FundaClient> logger)
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

		var pages = new List<Page>();
		while (await this.Timer.WaitForNextTickAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
		{
			var page = await this.GetRealEstateFromPageAsync(filter, currentPage, cancellationToken);

			pages.Add(page);
			
			this.Logger.Log(LogLevel.Information, "Stored {showCount} estates from page {page}.", page.Objects.Count, currentPage);

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
				estates: new AgencyEstates(value: group.ToList().Select(_ => new Estate(ranker.City, ranker.EstateHasGarden)).ToImmutableList()))
				{ Id = new(group.Key.Id) });
	}

	private async Task<Page> GetRealEstateFromPageAsync(EstateSupplyFilter filter, int pageNumber, CancellationToken cancellationToken)
	{
		this.Logger.Log(LogLevel.Information, "Retrieving shows from page {page}.", pageNumber);

		var pageSize = this.Settings.Value.PageSize;
		
		var request = new RestRequest
			{
				Method = Method.Get,
				RequestFormat = DataFormat.Json,
			}
			.AddParameter(EstateSupplyQueryParameter.Page.Value, pageNumber)
			.AddParameter(EstateSupplyQueryParameter.PageSize.Value, pageSize)
			.AddParameter(EstateSupplyQueryParameter.Type.Value, filter.SupplyType);

		if (filter.QueryString is not null)
			request.AddParameter(EstateSupplyQueryParameter.Query.Value, filter.QueryString);

		var page = await this.HttpClient.GetSingleAsync<Page>(request, cancellationToken)
			?? throw new InvalidDataException($"Received data from Funda page {pageNumber} (with size {pageSize}) for {filter.Name} is null.");
		
		return page;
	}
}
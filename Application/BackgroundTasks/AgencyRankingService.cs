using Fundalyzer.Domain.Agencies.Ranked;
using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fundalyzer.Application.BackgroundTasks;

/// <summary>
/// A background service which retrieves real estate supply and orders / filters it based on an <see cref="IAgencyRanker"/>.
/// </summary>
public sealed class AgencyRankingService : BackgroundService
{
	private IAgencyRanker AgencyRanker { get; }
	private ILogger<AgencyRankingService> Logger { get; }
	
	/// <summary>
	/// Is null when the service has not been completed yet.
	/// </summary>
	public RankedAgenciesResult? Result { get; private set; }

	public AgencyRankingService(IAgencyRanker agencyRanker, ILogger<AgencyRankingService> logger)
	{
		this.AgencyRanker = agencyRanker ?? throw new ArgumentNullException(nameof(agencyRanker));
		this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		=> await this.Synchronize(cancellationToken);

	// Wrapped because it makes it easier to unit test (so the task won't be executed in the background).
	internal async Task Synchronize(CancellationToken cancellationToken)
	{
		this.Logger.Log(LogLevel.Information, "{job}: Starting background job for {ranker}.", nameof(AgencyRankingService), this.AgencyRanker.GetType().Name);
		
		this.Result = await this.AgencyRanker.ListAndRankAsync(cancellationToken);
		
		this.Logger.Log(LogLevel.Information, "{job}: Ended background job for {ranker}.", nameof(AgencyRankingService), this.AgencyRanker.GetType().Name);
	}
}
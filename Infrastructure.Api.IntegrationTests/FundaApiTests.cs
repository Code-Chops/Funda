using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Infrastructure.Api.IntegrationTests;

public class FundaApiTests
{
    private IAgencyRanker Ranker { get; }
    private IEstateSupplyRepo FundaClient { get; }

    public FundaApiTests(IAgencyRanker ranker, IEstateSupplyRepo fundaClient)
    {
        this.Ranker = ranker;
        this.FundaClient = fundaClient;
    }

    /// <summary>
    /// Tests if the retrieval of show main information of show #1 is not null.
    /// </summary>
    [Fact]
    public async void GetShowMainInformationAsync_WithGarden_Success()
    {
        // arrange
        var ranker = new AgenciesHavingMostEstatesWithGardenInAms(this.FundaClient); 
        
        // act
        var response = await this.FundaClient.GetRealEstateAgenciesAsync(ranker, CancellationToken.None);

        // assert
        response.Should().NotBeNull();
    }

    /// <summary>
    /// Tests if retrieving the first page of shows is not null or empty.
    /// </summary>
    [Fact]
    public async void GetShowMainInformationAsync_WithoutGarden_Success()
    {
        // arrange 
        var ranker = new AgenciesHavingMostEstatesWithoutGardenInAms(this.FundaClient); 

        // act
        var response = await this.FundaClient.GetRealEstateAgenciesAsync(ranker, CancellationToken.None);

        // assert
        response.Should().NotBeNull().And.NotBeEmpty();
    }
}
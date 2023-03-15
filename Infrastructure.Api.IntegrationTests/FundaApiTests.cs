using Fundalyzer.Domain.Agencies.Ranked.Rankers;
using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Infrastructure.Api.IntegrationTests;

public class FundaApiTests
{
    private IEstateSupplyRepo FundaClient { get; }

    public FundaApiTests(IEstateSupplyRepo fundaClient)
    {
        this.FundaClient = fundaClient;
    }

    /// <summary>
    /// Tests if the retrieval of data is not null.
    /// </summary>
    [Fact]
    public async void GetInformationAsync_WithGarden_Success()
    {
        // arrange
        var ranker = new AgenciesHavingMostEstatesWithGardenInAmsterdam(this.FundaClient); 
        
        // act
        var response = await this.FundaClient.GetRealEstateAgenciesAsync(ranker, CancellationToken.None);

        // assert
        response.Should().NotBeNull();
    }
}
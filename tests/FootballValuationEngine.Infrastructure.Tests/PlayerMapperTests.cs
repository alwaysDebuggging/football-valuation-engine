using FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;
using FootballValuationEngine.Infrastructure.ExternalServices.Football;
using System.Text.Json;
using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;


namespace FootballValuationEngine.Infrastructure.Tests;

public class PlayerMapperTests
{
    private static Player LoadBellingham()
    {
        var json = File.ReadAllText(Path.Combine("Fixtures", "bellingham-2023.json"));
        var response = JsonSerializer.Deserialize<ApiPlayersResponse>(json)!;
        var mapper = new PlayerMapper();
        return mapper.ToDomain(response.Response[0]);
    }

    [Fact]
    public void ToDomain_RealBellinghamResponse_MapsProfileCorrectly()
    {

        var player = LoadBellingham();

        Assert.Equal(129718, player.Id);
        Assert.Equal("J. Bellingham", player.Name);
        Assert.Equal(22, player.Age);
        Assert.Equal(186, player.HeightCm);
        Assert.Equal(75, player.WeightKg);
        Assert.False(player.Injured);


    }


    [Fact]
    public void ToDomain_RealBellinghamResponse_MapsStatisticsCorrectly()
    {
        var player = LoadBellingham();

        Assert.Equal(8, player.Statistics.Count);

        var laLigaStats = player.Statistics.First(s => s.League.Name == "La Liga");

        Assert.Equal(2324, laLigaStats.Minutes);
        Assert.Equal(19, laLigaStats.Goals);
        Assert.Equal(6, laLigaStats.Assists);
        Assert.Equal(Position.Midfielder, laLigaStats.Position);
    }

    [Fact]
    public void ToDomain_RealBellinghamResponse_TeamNameMapsCorrectly()
    {
        var player = LoadBellingham();

        var laLigaStats = player.Statistics.First(s => s.League.Name == "La Liga");

        Assert.Equal("Real Madrid", laLigaStats.Team.Name);
    }

    [Fact]
    public void ToDomain_RealBellinghamResponse_PlayedStatisticsExcludesZeroMinuteEntries()
    {

        var player = LoadBellingham();

        Assert.True(player.PlayedStatistics.Count() < player.Statistics.Count());
        Assert.Contains(player.PlayedStatistics, statistics => statistics.Minutes > 0);



    }

}

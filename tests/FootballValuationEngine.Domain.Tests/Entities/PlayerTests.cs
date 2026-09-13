using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;


namespace FootballValuationEngine.Domain.Tests.Entities;

public class PlayerTests
{
    private readonly Player _validTestPlayer;

    public PlayerTests()
    {
        // Runs fresh before every test method — xUnit's equivalent of [SetUp]
        _validTestPlayer = new Player(
            id: 1,
            name: "Test Player",
            firstName: "Test",
            lastName: "Player",
            age: 25,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        );
    }

    private static PlayerStats CreateValidStats(int minutes)
    {
        return new PlayerStats(
            team: new Team(1, "Test Team"),
            league: new League(1, "Test League", "Test Country", 2023),
            position: Position.Midfielder,
            appearances: 0,
            minutes: minutes,
            rating: 7.5m,
            goals: 3,
            assists: 2,
            shotsTotal: 20,
            shotsOnTarget: 10,
            passesTotal: 500,
            passesKey: 15,
            passAccuracy: 85.5m,
            tacklesTotal: 12,
            interceptions: 8,
            duelsTotal: 100,
            duelsWon: 60,
            dribblesAttempts: 30,
            dribblesSuccess: 18,
            yellowCards: 2,
            redCards: 0);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    public void Constructor_IdZeroOrNegative_ThrowsArgumentOutOfRangeException(int id)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(
            id: id,
            name: "Test Player",
            firstName: "Test",
            lastName: "Player",
            age: 25,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        ));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_NullOrWhitespaceName_ThrowsArgumentException(string? name)
    {
        Assert.Throws<ArgumentException>(() => new Player(
            id: 1,
            name: name!,
            firstName: "Test",
            lastName: "Player",
            age: 25,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        ));
    }

    [Theory]
    [InlineData(13)]
    [InlineData(56)]
    public void Constructor_AgeBelowFourteenAndGreaterThen55_ThrowsArgumentOutOfRangeException(int age)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(
            id: 1,
            name: "Test Player",
            firstName: "Test",
            lastName: "Player",
            age: age,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        ));
    }

    [Theory]
    [InlineData(14)]
    [InlineData(55)]
    public void Constructor_ValidAge_DoesNotThrow(int age)
    {
        var validTestPlayer = new Player(
            id: 1,
            name: "Test Player",
            firstName: "Test",
            lastName: "Player",
            age: age,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        );

        Assert.NotNull(validTestPlayer);
        Assert.Equal(age, validTestPlayer.Age);
    }

    [Fact]
    public void AddStatistics_AddsToStatisticsCollection()
    {
        var playerStatsTest = CreateValidStats(minutes: 500);

        _validTestPlayer.AddStatistics(playerStatsTest);

        Assert.Single(_validTestPlayer.Statistics);
    }

    [Fact]
    public void PlayedStatistics_FiltersOutZeroMinuteEntries()
    {
        var played = CreateValidStats(minutes: 500);
        var unplayed = CreateValidStats(minutes: 0);

        _validTestPlayer.AddStatistics(played);
        _validTestPlayer.AddStatistics(unplayed);

        var playedStatistics = _validTestPlayer.PlayedStatistics.ToList();

        Assert.Single(playedStatistics);
        Assert.Contains(played, playedStatistics);
        Assert.DoesNotContain(unplayed, playedStatistics);
    }

    [Fact]
    public void Statistics_IsReadOnly_CannotBeModifiedExternally()
    {
        var playerStatsTest = CreateValidStats(minutes: 500);

        _validTestPlayer.AddStatistics(playerStatsTest);

        Assert.IsAssignableFrom<IReadOnlyCollection<PlayerStats>>(_validTestPlayer.Statistics);
        Assert.IsNotType<List<PlayerStats>>(_validTestPlayer.Statistics);
    }
}

using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;


namespace FootballValuationEngine.Domain.Tests.Entities;

public class PlayerStatsTests
{
    // Arrange Setup
    private static readonly Team ValidTeam = new(1, "Test Team");
    private static readonly League ValidLeague = new(1, "Test League", "Test Country", 2023);

    private static PlayerStats CreateValidStats(
        int appearances = 0,
        int minutes = 0,
        int? goals = 3,
        int? assists = 2,
        int? shotsTotal = 20,
        int? shotsOnTarget = 10,
        int? passesTotal = 500,
        int? passesKey = 15,
        decimal? passAccuracy = 85.5m,
        int? tacklesTotal = 12,
        int? interceptions = 8,
        int? duelsTotal = 100,
        int? duelsWon = 60,
        int? dribblesAttempts = 30,
        int? dribblesSuccess = 18,
        int yellowCards = 2,
        int redCards = 0)
    {
        return new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: appearances,
            minutes: minutes,
            rating: 7.5m,
            goals: goals,
            assists: assists,
            shotsTotal: shotsTotal,
            shotsOnTarget: shotsOnTarget,
            passesTotal: passesTotal,
            passesKey: passesKey,
            passAccuracy: passAccuracy,
            tacklesTotal: tacklesTotal,
            interceptions: interceptions,
            duelsTotal: duelsTotal,
            duelsWon: duelsWon,
            dribblesAttempts: dribblesAttempts,
            dribblesSuccess: dribblesSuccess,
            yellowCards: yellowCards,
            redCards: redCards);
    }

    // Test cases for PlayerStats constructor validation
    [Fact]
    public void Constructor_NegativeApperance_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            CreateValidStats(appearances: -1, minutes: 2000));
    }

    [Fact]
    public void Constructor_NegativeMinutes_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            CreateValidStats(appearances: 200, minutes: -1));
    }

    [Fact]
    public void Constructor_ValidMinutesAndAppearance_DoesNotThrow()
    {
        var plStats = CreateValidStats(appearances: 0, minutes: 0);

        Assert.NotNull(plStats);
        Assert.Equal(0, plStats.Appearances);
        Assert.Equal(0, plStats.Minutes);
    }

    [Fact]
    public void HasPlayed_MinutesGreaterThenZero_ReturnTrue()
    {
        var plStats = CreateValidStats(appearances: 0, minutes: 1);

        Assert.True(plStats.HasPlayed());
    }

    [Fact]
    public void HasPlayed_ZeroMinutes_ReturnFalse()
    {
        var plStats = CreateValidStats(appearances: 20, minutes: 0);

        Assert.False(plStats.HasPlayed());
    }

    [Fact]
    public void Constructor_NullableStatsFieldsAsNull_DoesNotThrow()
    {
        var plStats = CreateValidStats(
            appearances: 20,
            minutes: 0,
            goals: null,
            assists: null,
            shotsTotal: null,
            shotsOnTarget: null,
            passesTotal: null,
            passesKey: null,
            passAccuracy: null,
            tacklesTotal: null,
            interceptions: null,
            duelsTotal: null,
            duelsWon: null,
            dribblesAttempts: null,
            dribblesSuccess: null);

        Assert.NotNull(plStats);
        Assert.Null(plStats.Goals);
        Assert.Null(plStats.Assists);
        Assert.Null(plStats.ShotsTotal);
        Assert.Null(plStats.ShotsOnTarget);
        Assert.Null(plStats.PassesTotal);
        Assert.Null(plStats.PassesKey);
        Assert.Null(plStats.PassAccuracy);
        Assert.Null(plStats.TacklesTotal);
        Assert.Null(plStats.Interceptions);
        Assert.Null(plStats.DuelsTotal);
        Assert.Null(plStats.DuelsWon);
        Assert.Null(plStats.DribblesAttempts);
        Assert.Null(plStats.DribblesSuccess);
    }
}

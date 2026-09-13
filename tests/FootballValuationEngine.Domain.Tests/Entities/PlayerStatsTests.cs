using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;
namespace FootballValuationEngine.Domain.Tests;

public class PlayerStatsTests
{
    // Arrange Setup
    private static Team ValidTeam = new Team(1, "Test Team");
    private static League ValidLeague = new League(1, "Test League", "Test Country", 2023);


    // Test cases for PlayerStats constructor validation
    [Fact]
    public void Constructor_NegativeApperance_ThrowsArgumentOutOfRangeException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: -1,
            minutes: 2000,
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
            redCards: 0));
    }

    [Fact]
    public void Constructor_NegativeMinutes_ThrowsArgumentOutOfRangeException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: 200,
            minutes: -1,
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
            redCards: 0));
    }

    [Fact]
    public void Constructor_ValidMinutesAndApperance_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        PlayerStats PlStats = new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: 0,
            minutes: 0,
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


        // Act & Assert
        Assert.NotNull(PlStats);
    }

    [Fact]
    public void HasPlayed_MinutesGreaterThenZero_ReturnTrue()
    {
        // Arrange
        PlayerStats PlStats = new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: 0,
            minutes: 1,
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

        // Act & Assert
        Assert.True(PlStats.HasPlayed());
    }


    [Fact]
    public void HasPlayed_ZeroMinutes_ReturnFalse()
    {
        // Arrange
        PlayerStats PlStats = new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: 20,
            minutes: 0,
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

        // Act & Assert
        Assert.False(PlStats.HasPlayed());
    }


    [Fact]
    public void Constructor_NullableStatsFieldsAsNull_DoesNotThrow()
    {
        // Arrange
        PlayerStats PlStats = new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: 20,
            minutes: 0,
            rating: 7.5m,
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
            dribblesSuccess: null,
            yellowCards: 2,
            redCards: 0);

        // Act & Assert
        Assert.NotNull(PlStats);
        Assert.Null(PlStats.Goals);
        Assert.Null(PlStats.Assists);
        Assert.Null(PlStats.ShotsTotal);
        Assert.Null(PlStats.ShotsOnTarget);
        Assert.Null(PlStats.PassesTotal);
        Assert.Null(PlStats.PassesKey);
        Assert.Null(PlStats.PassAccuracy);
        Assert.Null(PlStats.TacklesTotal);
        Assert.Null(PlStats.Interceptions);
        Assert.Null(PlStats.DuelsTotal);
        Assert.Null(PlStats.DuelsWon);
        Assert.Null(PlStats.DribblesAttempts);
        Assert.Null(PlStats.DribblesSuccess);

    }

}

using System.Runtime.CompilerServices;
using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;
using Xunit;
namespace FootballValuationEngine.Domain.Tests.Entities;

public class PlayerTests
{
    // Arrange Setup
    private static Team ValidTeam = new Team(1, "Test Team");
    private static League ValidLeague = new League(1, "Test League", "Test Country", 2023);

    private static PlayerStats ValidPlayerStats(int appearances = 25, int minutes = 2000)
    {

        return new PlayerStats(
            team: ValidTeam,
            league: ValidLeague,
            position: Position.Midfielder,
            appearances: appearances,
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

    // Test cases for PlayerStats constructor validation
    [Theory]
    [InlineData(-5)]
    [InlineData(0)]

    public void Constructor_IdZeroOrNegative_ThrowsArgumentOutOfRangeException(int Id)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(
            id: Id,
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
    public void Constructor_NullOrWhitespaceName_ThrowsArgumentException(string Name)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new Player(
            id: 1,
            name: Name,
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

    [Fact]
    public void Constructor_AgeBelowFourteen_ThrowsArgumentOutOfRangeException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(
            id: 1,
            name: "Test Player",
            firstName: "Test",
            lastName: "Player",
            age: 13,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        ));
    }

    [Fact]
    public void Constructor_AgeAbove55_ThrowsArgumentOutOfRangeException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Player(
            id: 1,
            name: "Test Player",
            firstName: "Test",
            lastName: "Player",
            age: 56,
            nationality: "Testland",
            heightCm: 180,
            weightKg: 75,
            injured: false,
            photoUrl: "http://example.com/photo.jpg"
        ));
    }


    // Constructor_ValidAge_DoesNotThrow
    // AddStatistics_AddsToStatisticsCollection
    // PlayedStatistics_FiltersOutZeroMinuteEntries 
    // Statistics_IsReadOnly_CannotBeModifiedExternally





}

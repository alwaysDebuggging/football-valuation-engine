using FootballValuationEngine.Application.MarketValue;
using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;

namespace FootballValuationEngine.Application.Tests;

public class FormulaMarketValueCalculatorTests
{
    private static readonly Team TestTeam = new(1, "Test FC");
    private static readonly League TestLeague = new(1, "Test League", "Testland", 2023);

    private static Player CreatePlayer(int age = 25)
    {
        return new Player(
            id: 1,
            name: "Test Player",
            firstName: null,
            lastName: null,
            age: age,
            nationality: null,
            heightCm: null,
            weightKg: null,
            injured: false,
            photoUrl: null);
    }

    private static PlayerStats CreateStats(
        Position position = Position.Unknown,
        int minutes = 900,
        decimal? rating = null,
        int? goals = null,
        int? assists = null,
        int? passesKey = null,
        int? tacklesTotal = null,
        int? interceptions = null,
        int? duelsWon = null)
    {
        return new PlayerStats(
            team: TestTeam,
            league: TestLeague,
            position: position,
            appearances: 1,
            minutes: minutes,
            rating: rating,
            goals: goals,
            assists: assists,
            shotsTotal: null,
            shotsOnTarget: null,
            passesTotal: null,
            passesKey: passesKey,
            passAccuracy: null,
            tacklesTotal: tacklesTotal,
            interceptions: interceptions,
            duelsTotal: null,
            duelsWon: duelsWon,
            dribblesAttempts: null,
            dribblesSuccess: null,
            yellowCards: 0,
            redCards: 0);
    }

    [Fact]
    public void Calculate_NoPlayedStatistics_ReturnsZeroAndUnreliable()
    {
        // A player with no statistics at all has nothing to base a value on --
        // PlayedStatistics is empty, so Calculate should short-circuit
        // rather than divide by zero anywhere downstream.
        var player = CreatePlayer();
        var calculator = new FormulaMarketValueCalculator();

        var estimate = calculator.Calculate(player);

        Assert.Equal(0m, estimate.EstimatedValueEur);
        Assert.False(estimate.IsReliable);
    }

    [Fact]
    public void Calculate_BelowReliabilityThreshold_StillReturnsValueButFlaggedUnreliable()
    {
        // 200 minutes is well under the 450-minute reliability threshold,
        // but the calculator should still produce a (discounted) estimate
        // rather than refusing to compute one.
        var player = CreatePlayer();
        player.AddStatistics(CreateStats(minutes: 200));
        var calculator = new FormulaMarketValueCalculator();

        var estimate = calculator.Calculate(player);

        Assert.False(estimate.IsReliable);
        Assert.True(estimate.EstimatedValueEur > 0m);
    }

    [Fact]
    public void Calculate_NoRatingData_UsesNeutralMultiplier()
    {
        // Every other multiplier is pinned to a known constant here, so if
        // the rating multiplier is the expected neutral 1.0 for a player
        // with no rating data, the final value is exactly the baseline:
        //  - Position.Unknown -> performanceMultiplier is fixed at 1.0
        //  - age 25 (prime years, 24-27)-> ageCurve is fixed at 1.0
        //  - exactly 900 minutes -> reliabilityMultiplier is fixed at 1.0
        //  - rating: null -> ratingMultiplier should be the neutral 1.0
        // baseValue = 10,000,000m (Unknown baseline) * 1.0 (age) = 10,000,000m
        // finalValue = 10,000,000m * 1.0 * 1.0 * 1.0 = 10,000,000m
        var player = CreatePlayer(age: 25);
        player.AddStatistics(CreateStats(position: Position.Unknown, minutes: 900, rating: null));
        var calculator = new FormulaMarketValueCalculator();

        var estimate = calculator.Calculate(player);

        Assert.Equal(10_000_000m, estimate.EstimatedValueEur);
        Assert.True(estimate.IsReliable);
    }
}

using FootballValuationEngine.Application.MarketValue;
using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;

namespace FootballValuationEngine.Application.Tests;

/// <summary>
/// Exercises FormulaMarketValueCalculator with representative Bellingham
/// domain data, without depending on the Infrastructure test project.
/// </summary>
public class FormulaMarketValueCalculatorBellinghamTests
{
    [Fact]
    public void Calculate_BellinghamDomainData_ProducesReasonableEstimate()
    {
        var player = CreateBellinghamPlayer();
        var calculator = new FormulaMarketValueCalculator();

        var estimate = calculator.Calculate(player);

        Assert.True(estimate.IsReliable);

        // Not an exact pinned value -- the ScalingFactor and weightings are
        // still being hand-tuned. This range just confirms the formula is in
        // the right ballpark (current tuning lands Bellingham around ~14.9M)
        // and should be revisited if the weights change meaningfully.
        Assert.True(estimate.EstimatedValueEur > 10_000_000m);
        Assert.True(estimate.EstimatedValueEur < 20_000_000m);
    }

    private static Player CreateBellinghamPlayer()
    {
        var player = new Player(
            id: 129718,
            name: "J. Bellingham",
            firstName: "Jude",
            lastName: "Bellingham",
            age: 22,
            nationality: "England",
            heightCm: 186,
            weightKg: 75,
            injured: false,
            photoUrl: null);

        player.AddStatistics(new PlayerStats(
            team: new Team(541, "Real Madrid"),
            league: new League(140, "La Liga", "Spain", 2023),
            position: Position.Midfielder,
            appearances: 28,
            minutes: 2324,
            rating: null,
            goals: 19,
            assists: 6,
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
            yellowCards: 0,
            redCards: 0));

        return player;
    }
}

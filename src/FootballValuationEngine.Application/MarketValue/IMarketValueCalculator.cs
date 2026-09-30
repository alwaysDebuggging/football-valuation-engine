using FootballValuationEngine.Domain.Entities;

namespace FootballValuationEngine.Application.MarketValue;

/// <summary>
/// Estimates a player's market value from their statistics.
/// Implementations decide how stats translate into a value; callers
/// only need to know it takes a Player and returns an estimate.
/// </summary>
public interface IMarketValueCalculator
{

    /// <summary>
    /// Calculates a market value estimate for the given player based on
    /// their recorded statistics.
    /// </summary>
    /// <param name="player">The player to evaluate.</param>
    /// <returns>
    /// A <see cref="MarketValueEstimate"/> containing the estimated value
    /// and whether it's backed by enough playing time to be trusted.
    /// </returns>
    MarketValueEstimate Calculate(Player player);

}


namespace FootballValuationEngine.Application.MarketValue;


/// <summary>
/// Result of a market value calculation for a player.
/// </summary>
/// <param name="EstimatedValueEur">The estimated market value, in euros.</param>
/// <param name="IsReliable">
/// Whether the estimate is backed by enough playing time to be trusted.
/// False for players with fewer than 450 minutes played — the number is
/// still returned, but callers should treat it as a rough guess rather
/// than a confident valuation.
/// </param>
public record MarketValueEstimate (decimal EstimatedValueEur, bool IsReliable);

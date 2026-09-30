using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;

namespace FootballValuationEngine.Application.MarketValue;

/// <summary>
/// Estimates market value using a hand-tuned formula:
/// a position/age baseline, adjusted by per-90 performance, how much
/// playing time backs the numbers, and average match rating.
/// </summary>
public class FormulaMarketValueCalculator : IMarketValueCalculator
{
    // Tunable knob for how much performance can move the base value.
    // 0.1 keeps performance as a moderate adjustment rather than letting
    // it dominate the baseline — raise it to make stats matter more.
    private const decimal ScalingFactor = 0.1m;

    // Starting value per position before age and performance are applied.
    // Attacker > Midfielder > Defender > Goalkeeper roughly follows how
    // the transfer market prices positions; Unknown gets a neutral middle value.
    private static readonly Dictionary<Position, decimal> PositionBaseline = new()
    {
        [Position.Goalkeeper] = 8_000_000m,
        [Position.Defender] = 12_000_000m,
        [Position.Midfielder] = 15_000_000m,
        [Position.Attacker] = 18_000_000m,
        [Position.Unknown] = 10_000_000m
    };

    public MarketValueEstimate Calculate(Player player)
    {
        // Only stats where the player actually took the field are meaningful
        // for a per-90 style calculation; PlayerStats.HasPlayed already
        // filters that (Minutes > 0) via Player.PlayedStatistics.
        var playedStats = player.PlayedStatistics.ToList();

        if (playedStats.Count == 0)
        {
            // No playing time at all across any season/league -> nothing to base a value on.
            return new MarketValueEstimate(0m, false);
        }

        // --- Step 1: aggregate totals across all played statistics ---
        var totalMinutes = playedStats.Sum(s => s.Minutes);

        if (totalMinutes == 0)
        {
            // Defensive check: PlayedStatistics should already guarantee this,
            // but guards against dividing by zero below if that ever changes.
            return new MarketValueEstimate(0m, false);
        }

        // Nullable stat fields (not tracked for every competition/season) count as 0.
        var totalGoals = playedStats.Sum(s => s.Goals ?? 0);
        var totalAssists = playedStats.Sum(s => s.Assists ?? 0);
        var totalKeyPasses = playedStats.Sum(s => s.PassesKey ?? 0);
        var totalTackles = playedStats.Sum(s => s.TacklesTotal ?? 0);
        var totalInterceptions = playedStats.Sum(s => s.Interceptions ?? 0);
        var totalDuelsWon = playedStats.Sum(s => s.DuelsWon ?? 0);

        // --- Step 2: primary position = the stat line with the most minutes ---
        // Handles players who switched teams/leagues mid-season by trusting
        // wherever they actually played the most.
        var primaryPosition = playedStats
            .OrderByDescending(s => s.Minutes)
            .First()
            .Position;

        // --- Step 3: per-90 rates, so a part-season player and a full-season
        // player are compared on the same footing rather than raw totals ---
        var goalsPer90 = PerNinety(totalGoals, totalMinutes);
        var assistsPer90 = PerNinety(totalAssists, totalMinutes);
        var keyPassesPer90 = PerNinety(totalKeyPasses, totalMinutes);
        var tacklesPer90 = PerNinety(totalTackles, totalMinutes);
        var interceptionsPer90 = PerNinety(totalInterceptions, totalMinutes);
        var duelsWonPer90 = PerNinety(totalDuelsWon, totalMinutes);

        // --- Step 4: base value = position baseline scaled by an age curve ---
        // Young players are discounted (unproven/development risk), players
        // in their prime (24-27) get the full baseline, and value tapers off
        // past 30.
        var baseValue = PositionBaseline[primaryPosition] * AgeCurve(player.Age);

        // --- Step 5: performance multiplier, weighted by what matters for the position ---
        var performanceMultiplier = CalculatePerformanceMultiplier(
            primaryPosition,
            goalsPer90,
            assistsPer90,
            keyPassesPer90,
            tacklesPer90,
            interceptionsPer90,
            duelsWonPer90);

        // --- Step 6: reliability multiplier, discounting small sample sizes ---
        var reliabilityMultiplier = CalculateReliabilityMultiplier(totalMinutes);

        // --- Step 7: rating multiplier, rewarding/penalizing vs. an average (6.0) match rating ---
        var ratingMultiplier = CalculateRatingMultiplier(playedStats);

        // --- Step 8: combine everything into the final estimate ---
        var finalValue = baseValue * performanceMultiplier * reliabilityMultiplier * ratingMultiplier;

        // A player only counts as "reliable" once they've cleared 450 minutes
        // (roughly five full matches) — below that, the number is still
        // returned but flagged as a rough estimate.
        var isReliable = totalMinutes >= 450;

        return new MarketValueEstimate(finalValue, isReliable);
    }

    /// <summary>Converts a raw total into a "per 90 minutes played" rate.</summary>
    private static decimal PerNinety(int statTotal, int totalMinutes)
    {
        return (decimal)statTotal / totalMinutes * 90m;
    }

    /// <summary>
    /// Multiplier applied to the position baseline based on age.
    /// Peaks at 1.0 for ages 24-27, discounts younger (unproven) and
    /// older (declining) players.
    /// </summary>
    private static decimal AgeCurve(int age)
    {
        if (age < 21)
        {
            // Steepest discount: young, unproven, still developing.
            return 0.6m + (age - 16) * 0.08m;
        }

        if (age <= 23)
        {
            // Rising quickly as a player approaches their prime.
            return 0.85m + (age - 21) * 0.075m;
        }

        if (age <= 27)
        {
            // Prime years: full baseline value.
            return 1.0m;
        }

        if (age <= 30)
        {
            // Gentle decline as a player moves past their peak.
            return 1.0m - (age - 27) * 0.05m;
        }

        // Steeper decline past 30, floored so value never hits zero from age alone.
        return Math.Max(0.1m, 0.85m - (age - 30) * 0.08m);
    }

    /// <summary>
    /// Combines position-specific per-90 stats into a single multiplier
    /// on top of the base value. Each position weighs the stats that
    /// actually reflect its job on the pitch.
    /// </summary>
    private static decimal CalculatePerformanceMultiplier(
        Position position,
        decimal goalsPer90,
        decimal assistsPer90,
        decimal keyPassesPer90,
        decimal tacklesPer90,
        decimal interceptionsPer90,
        decimal duelsWonPer90)
    {
        decimal weightedSum;

        switch (position)
        {
            case Position.Attacker:
                // Goals matter most for an attacker, assists secondary.
                weightedSum = 0.6m * goalsPer90 + 0.4m * assistsPer90;
                break;
            case Position.Midfielder:
                // Creativity (assists, key passes) with some defensive work.
                weightedSum = 0.4m * assistsPer90 + 0.35m * keyPassesPer90 + 0.25m * tacklesPer90;
                break;
            case Position.Defender:
                // Defensive actions dominate: tackles, interceptions, duels won.
                weightedSum = 0.4m * tacklesPer90 + 0.35m * interceptionsPer90 + 0.25m * duelsWonPer90;
                break;
            case Position.Goalkeeper:
            case Position.Unknown:
            default:
                // Goalkeeper-specific stats (saves, clean sheets) aren't modeled
                // yet, and there's nothing meaningful to weigh for Unknown, so
                // performance is treated as neutral for now.
                return 1.0m;
        }

        // Scale down the weighted sum so performance nudges the base value
        // rather than swinging it wildly.
        return 1.0m + weightedSum * ScalingFactor;
    }

    /// <summary>
    /// Discounts the estimate when there isn't much playing time to back
    /// it up, ramping linearly from 0.8 at 450 minutes to a full 1.0 at
    /// 900 minutes. Below 450 minutes the same line is extended rather
    /// than capped, since the estimate is already flagged unreliable there.
    /// </summary>
    private static decimal CalculateReliabilityMultiplier(int totalMinutes)
    {
        if (totalMinutes >= 900)
        {
            return 1.0m;
        }

        return 0.8m + (decimal)(totalMinutes - 450) / 450m * 0.2m;
    }

    /// <summary>
    /// Adjusts the estimate based on average match rating, using 6.0 as a
    /// neutral baseline. Players with no rating data at all get a neutral
    /// multiplier instead of being penalized for missing data.
    /// </summary>
    private static decimal CalculateRatingMultiplier(IReadOnlyCollection<PlayerStats> playedStats)
    {
        var ratings = playedStats
            .Where(s => s.Rating.HasValue)
            .Select(s => s.Rating!.Value)
            .ToList();

        if (ratings.Count == 0)
        {
            return 1.0m;
        }

        var avgRating = ratings.Average();

        return 0.7m + (avgRating - 6.0m) * 0.15m;
    }
}

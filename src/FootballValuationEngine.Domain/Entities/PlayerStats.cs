using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;

namespace FootballValuationEngine.Domain.Entities;

public class PlayerStats
{
    public Team Team { get; }
    public League League { get; }
    public Position Position { get; }
    public int Appearances { get; }
    public int Minutes { get; }
    public decimal? Rating { get; }
    public int? Goals { get; }
    public int? Assists { get; }
    public int? ShotsTotal { get; }
    public int? ShotsOnTarget { get; }
    public int? PassesTotal { get; }
    public int? PassesKey { get; }
    public decimal? PassAccuracy { get; }
    public int? TacklesTotal { get; }
    public int? Interceptions { get; }
    public int? DuelsTotal { get; }
    public int? DuelsWon { get; }
    public int? DribblesAttempts { get; }
    public int? DribblesSuccess { get; }
    public int YellowCards { get; }
    public int RedCards { get; }

    public PlayerStats(
        Team team,
        League league,
        Position position,
        int appearances,
        int minutes,
        decimal? rating,
        int? goals,
        int? assists,
        int? shotsTotal,
        int? shotsOnTarget,
        int? passesTotal,
        int? passesKey,
        decimal? passAccuracy,
        int? tacklesTotal,
        int? interceptions,
        int? duelsTotal,
        int? duelsWon,
        int? dribblesAttempts,
        int? dribblesSuccess,
        int yellowCards,
        int redCards)
    {
        if (appearances < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(appearances), "Appearances cannot be negative.");
        }

        if (minutes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes cannot be negative.");
        }

        Team = team;
        League = league;
        Position = position;
        Appearances = appearances;
        Minutes = minutes;
        Rating = rating;
        Goals = goals;
        Assists = assists;
        ShotsTotal = shotsTotal;
        ShotsOnTarget = shotsOnTarget;
        PassesTotal = passesTotal;
        PassesKey = passesKey;
        PassAccuracy = passAccuracy;
        TacklesTotal = tacklesTotal;
        Interceptions = interceptions;
        DuelsTotal = duelsTotal;
        DuelsWon = duelsWon;
        DribblesAttempts = dribblesAttempts;
        DribblesSuccess = dribblesSuccess;
        YellowCards = yellowCards;
        RedCards = redCards;
    }

    public bool HasPlayed()
    {
        return Minutes > 0;
    }




}

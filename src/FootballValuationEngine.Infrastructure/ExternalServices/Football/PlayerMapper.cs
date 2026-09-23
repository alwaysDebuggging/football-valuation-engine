using System.Text.RegularExpressions;
using FootballValuationEngine.Domain.Entities;
using FootballValuationEngine.Domain.Enums;
using FootballValuationEngine.Domain.ValueObjects;
using FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football;


public class PlayerMapper
{

    public Player ToDomain(PlayerResponseItemDto dto)
    {
        var heightCm = ParseLeadingDigits(dto.Player.Height);
        var weightKg = ParseLeadingDigits(dto.Player.Weight);

        var player = new Player(
            id: dto.Player.Id,
            name: dto.Player.Name,
            firstName: dto.Player.Firstname,
            lastName: dto.Player.Lastname,
            age: dto.Player.Age,
            nationality: dto.Player.Nationality,
            heightCm: heightCm,
            weightKg: weightKg,
            injured: dto.Player.Injured,
            photoUrl: dto.Player.Photo
        );


        foreach (var statisticsDto in dto.Statistics)
        {
            var stats = MapStatistics(statisticsDto);
            player.AddStatistics(stats);
        }

        return player;
    }

    private PlayerStats MapStatistics(StatisticsDto dto)
    {
        var team = new Team(dto.Team.Id, dto.Team.Name);
        var league = new League(dto.League.Id, dto.League.Name, dto.League.Country, dto.League.Season);

        var position = MapPosition(dto.Games.Position);

        var appearances = dto.Games.Appearences ?? 0;

        var minutes = dto.Games.Minutes ?? 0;

        decimal? rating = decimal.TryParse(dto.Games.Rating, out var parsedRating)
            ? parsedRating
            : null;

        decimal? accuracy = decimal.TryParse(dto.Passes?.Accuracy, out var parsedAccuracy) ? parsedAccuracy : null;

        var yellowCards = dto.Cards?.Yellow ?? 0;
        var redCards = dto.Cards?.Red ?? 0;


        var playerStats = new PlayerStats(
            team: team,
            league: league,
            position: position,
            appearances: appearances,
            minutes: minutes,
            rating: rating,
            goals: dto.Goals?.Total,
            assists: dto.Goals?.Assists,
            shotsTotal: dto.Shots?.Total,
            shotsOnTarget: dto.Shots?.On,
            passesTotal: dto.Passes?.Total,
            passesKey: dto.Passes?.Key,
            passAccuracy: accuracy,
            tacklesTotal: dto.Tackles?.Total,
            interceptions: dto.Tackles?.Interceptions,
            duelsTotal: dto.Duels?.Total,
            duelsWon: dto.Duels?.Won,
            dribblesAttempts: dto.Dribbles?.Attempts,
            dribblesSuccess: dto.Dribbles?.Success,
            yellowCards: yellowCards,
            redCards: redCards

        );

        return playerStats;

    }

    private Position MapPosition(string? rawPosition)
    {
        return rawPosition?.Trim().ToLowerInvariant() switch
        {
            "goalkeeper" => Position.Goalkeeper,
            "defender" => Position.Defender,
            "midfielder" => Position.Midfielder,
            "attacker" => Position.Attacker,
            _ => Position.Unknown
        };
    }

    private static int? ParseLeadingDigits(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        var match = Regex.Match(raw, @"\d+");

        return match.Success && int.TryParse(match.Value, out var result) ? result : null;
    }
}

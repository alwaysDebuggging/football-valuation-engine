using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class StatisticsDto
{
    [JsonPropertyName("team")]
    public TeamDto Team { get; set; } = null!;

    [JsonPropertyName("league")]
    public LeagueDto League { get; set; } = null!;

    [JsonPropertyName("games")]
    public GamesDto Games { get; set; } = null!;

    [JsonPropertyName("shots")]
    public ShotsDto? Shots { get; set; }

    [JsonPropertyName("goals")]
    public GoalsDto? Goals { get; set; }

    [JsonPropertyName("passes")]
    public PassesDto? Passes { get; set; }

    [JsonPropertyName("tackles")]
    public TacklesDto? Tackles { get; set; }

    [JsonPropertyName("duels")]
    public DuelsDto? Duels { get; set; }

    [JsonPropertyName("dribbles")]
    public DribblesDto? Dribbles { get; set; }

    [JsonPropertyName("cards")]
    public CardsDto? Cards { get; set; }
}

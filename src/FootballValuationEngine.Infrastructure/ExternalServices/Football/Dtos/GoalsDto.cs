using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class GoalsDto
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("conceded")]
    public int? Conceded { get; set; }

    [JsonPropertyName("assists")]
    public int? Assists { get; set; }

    [JsonPropertyName("saves")]
    public int? Saves { get; set; }
}   

using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class GamesDto
{
    // API's own field is misspelled "appearences" - the JSON key must match exactly
    [JsonPropertyName("appearences")]
    public int? Appearences { get; set; }

    [JsonPropertyName("lineups")]
    public int? Lineups { get; set; }

    [JsonPropertyName("minutes")]
    public int? Minutes { get; set; }

    [JsonPropertyName("number")]
    public int? Number { get; set; }

    [JsonPropertyName("position")]
    public string? Position { get; set; }

    // Comes back as a string, e.g. "8.035714" (parsed in the mapper)
    [JsonPropertyName("rating")]
    public string? Rating { get; set; }

    [JsonPropertyName("captain")]
    public bool? Captain { get; set; }
}

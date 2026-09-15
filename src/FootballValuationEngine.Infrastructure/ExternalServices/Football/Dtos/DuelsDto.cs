using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class DuelsDto
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("won")]
    public int? Won { get; set; }
}

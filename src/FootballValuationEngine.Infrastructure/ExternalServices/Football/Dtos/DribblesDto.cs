using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class DribblesDto
{
    [JsonPropertyName("attempts")]
    public int? Attempts { get; set; }

    [JsonPropertyName("success")]
    public int? Success { get; set; }

    [JsonPropertyName("past")]
    public int? Past { get; set; }
}

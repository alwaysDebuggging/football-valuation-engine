using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class ShotsDto
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("on")]
    public int? On { get; set; }
}

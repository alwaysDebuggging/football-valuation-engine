using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class PlayerResponseItemDto
{
    [JsonPropertyName("player")]
    public PlayerProfileDto Player { get; set; } = null!;

    [JsonPropertyName("statistics")]
    public List<StatisticsDto> Statistics { get; set; } = new();
}

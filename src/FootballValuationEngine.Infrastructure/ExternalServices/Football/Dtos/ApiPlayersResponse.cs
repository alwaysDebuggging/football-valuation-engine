using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class ApiPlayersResponse
{
    [JsonPropertyName("get")]
    public string? Get { get; set; }

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new();

    [JsonPropertyName("results")]
    public int Results { get; set; }

    [JsonPropertyName("response")]
    public List<PlayerResponseItemDto> Response { get; set; } = new();
}

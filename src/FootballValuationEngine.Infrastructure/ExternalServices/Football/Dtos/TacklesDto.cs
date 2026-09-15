using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class TacklesDto
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("blocks")]
    public int? Blocks { get; set; }

    [JsonPropertyName("interceptions")]
    public int? Interceptions { get; set; }
}

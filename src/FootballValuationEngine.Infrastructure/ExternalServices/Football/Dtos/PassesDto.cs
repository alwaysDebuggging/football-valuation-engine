using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class PassesDto
{
    [JsonPropertyName("total")]
    public int? Total { get; set; }

    [JsonPropertyName("key")]
    public int? Key { get; set; }

    // Format is inconsistent across responses (sometimes numeric, sometimes string) -
    // kept as string here and parsed defensively in the mapper.
    [JsonPropertyName("accuracy")]
    public string? Accuracy { get; set; }
}

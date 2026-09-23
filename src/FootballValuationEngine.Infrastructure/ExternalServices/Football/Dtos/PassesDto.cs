using System.Text.Json.Serialization;
using FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos.Converters;

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
    [JsonConverter(typeof(FlexibleStringConverter))]
    public string? Accuracy { get; set; }
}

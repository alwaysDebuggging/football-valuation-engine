using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;


public class BirthDto
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("place")]
    public string? Place { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }


}

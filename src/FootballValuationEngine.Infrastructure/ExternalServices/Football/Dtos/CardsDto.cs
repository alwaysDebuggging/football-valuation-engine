using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class CardsDto
{

    [JsonPropertyName("yellow")]
    public string? Yellow { get; set; }

    [JsonPropertyName("yellowred")]
    public string? YellowRed { get; set; }

    [JsonPropertyName("Red")]
    public string? Red { get; set; }

}

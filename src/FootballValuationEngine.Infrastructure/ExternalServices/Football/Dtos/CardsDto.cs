using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class CardsDto
{

    [JsonPropertyName("yellow")]
    public int? Yellow { get; set; }

    [JsonPropertyName("yellowred")]
    public int? YellowRed { get; set; }

    [JsonPropertyName("red")]
    public int? Red { get; set; }

}

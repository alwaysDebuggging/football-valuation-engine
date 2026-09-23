using System.Text.Json.Serialization;

namespace FootballValuationEngine.Infrastructure.ExternalServices.Football.Dtos;

public class PlayerProfileDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("firstname")]
    public string? Firstname { get; set; }

    [JsonPropertyName("lastname")]
    public string? Lastname { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("birth")]
    public BirthDto? Birth { get; set; }

    [JsonPropertyName("nationality")]
    public string? Nationality { get; set; }

    // Comes back as a string like "186" or "186 cm"  (parsed in the mapper)
    [JsonPropertyName("height")]
    public string? Height { get; set; }

    // Comes back as a string like "75" or "75 kg" (parsed in the mapper)
    [JsonPropertyName("weight")]
    public string? Weight { get; set; }

    [JsonPropertyName("injured")]
    public bool Injured { get; set; }

    [JsonPropertyName("photo")]
    public string? Photo { get; set; }
}

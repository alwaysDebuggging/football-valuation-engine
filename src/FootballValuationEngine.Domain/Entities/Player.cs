
namespace FootballValuationEngine.Domain.Entities;


public class Player
{
    private readonly List<PlayerStats> _statistics = new List<PlayerStats>();

    public int Id { get; }
    public string Name { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public int Age { get; }
    public string? Nationality { get; }
    public int? HeightCm { get; }
    public int? WeightKg { get; }
    public bool Injured { get; }
    public string? PhotoUrl { get; }

    public IReadOnlyCollection<PlayerStats> Statistics => _statistics.AsReadOnly();

    public Player(
        int id,
        string name,
        string? firstName,
        string? lastName,
        int age,
        string? nationality,
        int? heightCm,
        int? weightKg,
        bool injured,
        string? photoUrl)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
        }

        if (age < 14 || age > 55)
        {
            throw new ArgumentOutOfRangeException(nameof(age), "Age must be between 14 and 55.");
        }

        Id = id;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Nationality = nationality;
        HeightCm = heightCm;
        WeightKg = weightKg;
        Injured = injured;
        PhotoUrl = photoUrl;
    }

    public void AddStatistics(PlayerStats statistics)
    {
        _statistics.Add(statistics);
    }

    public IEnumerable<PlayerStats> PlayedStatistics => _statistics.Where(s => s.HasPlayed());
}

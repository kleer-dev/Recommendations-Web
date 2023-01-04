namespace Recommendations.Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double AverageRate { get; set; }

    public Review Review { get; set; }
    public List<Rating> UserRatings { get; set; } = new();
}
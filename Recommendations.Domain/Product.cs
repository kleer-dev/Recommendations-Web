namespace Recommendations.Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double AverageRate { get; set; }

    public List<Review> Reviews { get; set; } = new();
    public List<Rating> UserRatings { get; set; } = new();
}
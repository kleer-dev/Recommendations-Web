namespace Recommendations.Domain;

public class Rating
{
    public Guid Id { get; set; }
    public double Value { get; set; }
    
    public User User { get; set; }
    public Product Product { get; set; }
}
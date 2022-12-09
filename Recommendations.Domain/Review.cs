namespace Recommendations.Domain;

public class Review
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; }
    public string ImageUrl { get; set; }
    
    public User User { get; set; }
    public List<Tag> Tags { get; set; }
    public Category Category { get; set; }
}
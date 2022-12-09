namespace Recommendations.Domain;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<Review> Reviews { get; set; }
}
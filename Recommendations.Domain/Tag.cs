namespace Recommendations.Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<Review> Reviews { get; set; }
}
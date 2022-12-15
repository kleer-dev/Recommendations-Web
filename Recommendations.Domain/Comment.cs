namespace Recommendations.Domain;

public class Comment
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationDate { get; set; }
    
    public Review Review { get; set; }
    public User User { get; set; }
}
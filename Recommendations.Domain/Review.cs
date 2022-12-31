using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendations.Domain;

public class Review
{
    [NotMapped]
    public string ObjectID { get; set; }
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int AuthorRate { get; set; }
    public DateTime CreationDate { get; set; }

    public User User { get; set; }
    public List<Tag> Tags { get; set; }
    public Category Category { get; set; }
    public Product Product { get; set; } = new();
    public List<Like> Likes { get; set; } = new();
    public List<Comment> Comments { get; set; }
    public List<Image>? Images { get; set; }
}
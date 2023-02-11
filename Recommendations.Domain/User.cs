using Microsoft.AspNetCore.Identity;

namespace Recommendations.Domain;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; }
    public int LikesCount { get; set; }
    public string AccessStatus { get; set; } 

    public List<Review> Reviews { get; set; } = new();
    public List<Rating> Ratings { get; set; } = new();
    public List<Like> Likes { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
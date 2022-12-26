using Microsoft.AspNetCore.Identity;

namespace Recommendations.Domain;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; }
    public int LikesCount { get; set; }
    
    public List<Review> Reviews { get; set; }
    public List<Rating> Ratings { get; set; }
    public List<Like> Likes { get; set; }
    public List<Comment> Comments { get; set; }
}
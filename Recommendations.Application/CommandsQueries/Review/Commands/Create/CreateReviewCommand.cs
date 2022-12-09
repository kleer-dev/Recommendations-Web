using MediatR;
using Microsoft.AspNetCore.Http;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommand : IRequest<Guid>
{
    public Guid? UserId { get; set; }
    public string Title { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; }
    public IFormFile? Image { get; set; }
    public string[] Tags { get; set; }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Tag.Commands.Create;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMegaCloudClient _megaCloudClient;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(IRecommendationsDbContext context,
        IMediator mediator, IMegaCloudClient megaCloudClient, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _megaCloudClient = megaCloudClient;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var createTagCommand = new CreateTagCommand { Tags = request.Tags };
        await _mediator.Send(createTagCommand, cancellationToken);
        
        var reviewId = await AddReview(request, cancellationToken);

        return reviewId;
    }

    private async Task<Guid> AddReview(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = _mapper.Map<Domain.Review>(request);
        review.User = await GetUser(request.UserId, cancellationToken);
        review.Tags = await GetTags(request, cancellationToken);
        review.Category = await GetCategory(request, cancellationToken);
        review.ImageUrl = await GetImageUrl(request.Image!);
        review.Product = new Domain.Product { Name = request.ProductName };
        review.CreationDate = DateTime.UtcNow;
        
        await _context.Reviews.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return review.Id;
    }

    private async Task<Domain.User> GetUser(Guid? userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user is null)
            throw new NullReferenceException("The user does not exist");

        return user;
    }

    private async Task<List<Domain.Tag>> GetTags(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
            .Where(t => request.Tags.Any(n => n == t.Name)).ToListAsync(cancellationToken);

        return tags;
    }

    private async Task<Domain.Category> GetCategory(CreateReviewCommand request, 
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == request.Category, cancellationToken);
        if (category is null)
            throw new NullReferenceException("The category not found");

        return category;
    }

    private async Task<string> GetImageUrl(IFormFile? file)
    {
        if (file is null)
            return string.Empty;
        
        var imageUrl = await _megaCloudClient.UploadFile(file);
        return imageUrl;
    }
}
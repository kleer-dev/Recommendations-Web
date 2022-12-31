using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Recommendations.Application.CommandsQueries.Category.Queries.GetByName;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.CommandsQueries.Tag.Commands.Create;
using Recommendations.Application.CommandsQueries.Tag.Queries.GetTagListByNames;
using Recommendations.Application.Common.Firebase;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Update;

public class UpdateReviewCommandHandler
    : IRequestHandler<UpdateReviewCommand, Unit>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IFirebaseService _firebaseService;

    public UpdateReviewCommandHandler(IRecommendationsDbContext context,
        IMediator mediator, IMapper mapper, IFirebaseService firebaseService)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
        _firebaseService = firebaseService;
    }

    public async Task<Unit> Handle(UpdateReviewCommand request,
        CancellationToken cancellationToken)
    {
        await AddMissingTags(request.Tags, cancellationToken);
        var review = await GetReview(request.ReviewId, cancellationToken);
        var updateReview = _mapper.Map(request, review);
        updateReview.Category = await GetCategory(request.CategoryName, cancellationToken);
        updateReview.Product.Name = request.ProductName;
        updateReview.Tags = await GetTagsByNames(request.Tags, cancellationToken);
        updateReview.Images = await UpdateImages(request.Images,
            review.Images?.FirstOrDefault()?.FolderName ?? Guid.NewGuid().ToString());

        _context.Reviews.Update(review);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewQuery = new GetReviewQuery
        {
            ReviewId = reviewId
        };
        return await _mediator.Send(getReviewQuery, cancellationToken);
    }

    private async Task<Domain.Category> GetCategory(string categoryName,
        CancellationToken cancellationToken)
    {
        var getCategoryByNameQuery = new GetCategoryByNameQuery
        {
            Name = categoryName
        };
        return await _mediator.Send(getCategoryByNameQuery, cancellationToken);
    }
    
    private async Task AddMissingTags(string[] tags,
        CancellationToken cancellationToken)
    {
        var createTagsCommand = new CreateTagCommand
        {
            Tags = tags
        };
        await _mediator.Send(createTagsCommand, cancellationToken);
    }

    private async Task<List<Domain.Tag>> GetTagsByNames(string[] tagNames,
        CancellationToken cancellationToken)
    {
        var getTagListByNameQuery = new GetTagListByNamesQuery
        {
            Tags = tagNames
        };
        var tags = await _mediator.Send(getTagListByNameQuery, cancellationToken);
        
        return tags.ToList();
    }

    private async Task<List<Domain.Image>> UpdateImages(IEnumerable<IFormFile> files, string folderName)
    {
        var imageData = await _firebaseService.UpdateFiles(files, folderName);
        return _mapper.Map<IEnumerable<ImageData>, List<Domain.Image>>(imageData);
    }
}
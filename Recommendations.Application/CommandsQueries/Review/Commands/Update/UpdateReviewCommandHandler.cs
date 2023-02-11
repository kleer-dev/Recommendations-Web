using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Recommendations.Application.CommandsQueries.Category.Queries.GetByName;
using Recommendations.Application.CommandsQueries.Product.Commands;
using Recommendations.Application.CommandsQueries.Product.Queries.GetByName;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.CommandsQueries.Tag.Commands.Create;
using Recommendations.Application.CommandsQueries.Tag.Queries.GetTagListByNames;
using Recommendations.Application.Common.Clouds.Firebase;
using Recommendations.Application.Interfaces;

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
        updateReview.Tags = await GetTagsByNames(request.Tags, cancellationToken);
        updateReview.Images = await UpdateImages(request.Images,
            review.Images?.FirstOrDefault()?.FolderName ?? Guid.NewGuid().ToString());
        updateReview.Product = await GetProduct(request.ProductName, cancellationToken)
                               ?? await CreateProduct(request.ProductName, cancellationToken);

        _context.Reviews.Update(updateReview);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
    
    private async Task<Domain.Product?> GetProduct(string productName,
        CancellationToken cancellationToken)
    {
        var getProductByNameQuery = new GetProductByNameQuery(productName);
        return await _mediator.Send(getProductByNameQuery, cancellationToken);
    }

    private async Task<Domain.Product> CreateProduct(string productName,
        CancellationToken cancellationToken)
    {
        var createProductCommand = new CreateProductCommand(productName);
        return await _mediator.Send(createProductCommand, cancellationToken);
    }

    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewQuery = new GetReviewQuery(reviewId);
        return await _mediator.Send(getReviewQuery, cancellationToken);
    }

    private async Task<Domain.Category> GetCategory(string categoryName,
        CancellationToken cancellationToken)
    {
        var getCategoryByNameQuery = new GetCategoryByNameQuery(categoryName);
        return await _mediator.Send(getCategoryByNameQuery, cancellationToken);
    }
    
    private async Task AddMissingTags(string[] tags,
        CancellationToken cancellationToken)
    {
        var createTagsCommand = new CreateTagCommand(tags);
        await _mediator.Send(createTagsCommand, cancellationToken);
    }

    private async Task<List<Domain.Tag>> GetTagsByNames(string[] tagNames,
        CancellationToken cancellationToken)
    {
        var getTagListByNameQuery = new GetTagListByNamesQuery(tagNames);
        var tags = await _mediator.Send(getTagListByNameQuery, cancellationToken);
        return tags.ToList();
    }

    private async Task<List<Domain.Image>> UpdateImages(IEnumerable<IFormFile> files,
        string folderName)
    {
        var imageData = await _firebaseService.UpdateFiles(files, folderName);
        return _mapper.Map<IEnumerable<ImageData>, List<Domain.Image>>(imageData);
    }
}
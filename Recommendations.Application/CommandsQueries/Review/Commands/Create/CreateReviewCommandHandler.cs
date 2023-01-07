using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Category.Queries.GetByName;
using Recommendations.Application.CommandsQueries.Product.Commands;
using Recommendations.Application.CommandsQueries.Product.Queries.GetByName;
using Recommendations.Application.CommandsQueries.Rating.Commands.Create;
using Recommendations.Application.CommandsQueries.Tag.Commands.Create;
using Recommendations.Application.CommandsQueries.Tag.Queries.GetTagListByNames;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Clouds.Firebase;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IFirebaseService _firebaseService;

    public CreateReviewCommandHandler(IRecommendationsDbContext context,
        IMediator mediator, IMapper mapper, IFirebaseService firebase)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
        _firebaseService = firebase;
    }

    public async Task<Guid> Handle(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        await AddTags(request.Tags, cancellationToken);
        var review = _mapper.Map<Domain.Review>(request);
        review.User = await GetUser(request.UserId, cancellationToken);
        review.Tags = await GetTags(request.Tags, cancellationToken);
        review.Category = await GetCategory(request.CategoryName, cancellationToken);
        review.Product = await GetProduct(request.ProductName, cancellationToken)
                         ?? await CreateProduct(request.ProductName, cancellationToken);
        review.CreationDate = DateTime.UtcNow;
        review.Images = await AddImages(request.Images);

        await _context.Reviews.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return review.Id;
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

    private async Task AddTags(string[] tags, CancellationToken cancellationToken)
    {
        var createTagCommand = new CreateTagCommand(tags);
        await _mediator.Send(createTagCommand, cancellationToken);
    }

    private async Task<Domain.User> GetUser(Guid userId,
        CancellationToken cancellationToken)
    {
        var getUserQuery = new GetUserQuery(userId);
        return await _mediator.Send(getUserQuery, cancellationToken);
    }

    private async Task<List<Domain.Tag>> GetTags(string[] tagNames,
        CancellationToken cancellationToken)
    {
        var getTagListByNamesQuery = new GetTagListByNamesQuery(tagNames);
        var tags = await _mediator.Send(getTagListByNamesQuery, cancellationToken);
        return tags.ToList();
    }

    private async Task<Domain.Category> GetCategory(string categoryName,
        CancellationToken cancellationToken)
    {
        var getCategoryByNameQuery = new GetCategoryByNameQuery(categoryName);
        return await _mediator.Send(getCategoryByNameQuery, cancellationToken);
    }

    private async Task<List<Domain.Image>> AddImages(IEnumerable<IFormFile> files)
    {
        var imageData = await _firebaseService.UploadFiles(files, Guid.NewGuid().ToString());
        return _mapper.Map<IEnumerable<ImageData>, List<Domain.Image>>(imageData);
    }
}
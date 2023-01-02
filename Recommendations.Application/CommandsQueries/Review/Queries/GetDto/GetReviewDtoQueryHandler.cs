using AutoMapper;
using MediatR;
using Recommendations.Application.CommandsQueries.Image.Queries.GetImageListByReviewId;
using Recommendations.Application.CommandsQueries.Like.Queries.GetLikeStatus;
using Recommendations.Application.CommandsQueries.Rating.Queries.GetUserRating;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDtoQueryHandler
    : IRequestHandler<GetReviewDtoQuery, GetReviewDto>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetReviewDtoQueryHandler(IRecommendationsDbContext context,
        IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<GetReviewDto> Handle(GetReviewDtoQuery request,
        CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        var getReviewDto = _mapper.Map<GetReviewDto>(review);
        getReviewDto.ImagesUrls = await GetImagesUrls(request.ReviewId, cancellationToken);
        getReviewDto.IsLike = await GetLikeStatus(request.UserId, request.ReviewId, cancellationToken);
        getReviewDto.UserRating = await GetUserRating(request.UserId, review.Product.Id, cancellationToken);

        return getReviewDto;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getReviewQuery = new GetReviewQuery(reviewId);
        return await _mediator.Send(getReviewQuery, cancellationToken);
    }

    private async Task<bool> GetLikeStatus(Guid? userId, Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getLikeStatus = new GetLikeStatusQuery(userId, reviewId);
        return await _mediator.Send(getLikeStatus, cancellationToken);
    }
    
    private async Task<double> GetUserRating(Guid? userId, Guid productId,
        CancellationToken cancellationToken)
    {
        var getRatingQuery = new GetUserRatingQuery(userId, productId);
        var rating = await _mediator.Send(getRatingQuery, cancellationToken);
        return rating?.Value ?? 1;
    }
    
    private async Task<List<string>?> GetImagesUrls(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getImagesByReviewIdQuery = new GetImageListByReviewIdQuery(reviewId);
        var images = await _mediator.Send(getImagesByReviewIdQuery, cancellationToken);
        return images?.Select(i => i.Url).ToList();
    }
}
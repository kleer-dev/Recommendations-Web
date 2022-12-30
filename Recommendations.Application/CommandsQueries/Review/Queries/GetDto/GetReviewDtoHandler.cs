using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.CommandsQueries.Image.Queries.GetImagesByReviewId;
using Recommendations.Application.CommandsQueries.Like.Queries.Get;
using Recommendations.Application.CommandsQueries.Rating.Queries.GetUserRating;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDtoQueryHandler : IRequestHandler<GetReviewDtoQuery, GetReviewDto>
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
        var getReviewQuery = new GetReviewQuery
        {
            ReviewId = reviewId
        };
        var review = await _mediator.Send(getReviewQuery, cancellationToken);

        return review;
    }

    private async Task<bool> GetLikeStatus(Guid? userId, Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getLikeStatus = new GetLikeQuery
        {
            UserId = userId,
            ReviewId = reviewId
        };
        var like = await _mediator.Send(getLikeStatus, cancellationToken);

        return like?.Status ?? false;
    }
    
    private async Task<double> GetUserRating(Guid? userId, Guid productId,
        CancellationToken cancellationToken)
    {
        var getRatingQuery = new GetUserRatingQuery
        {
            UserId = userId,
            ProductId = productId
        };
        var rating = await _mediator.Send(getRatingQuery, cancellationToken);

        return rating is null ? 1 : rating.Value;
    }
    
    private async Task<List<string>> GetImagesUrls(Guid reviewId,
        CancellationToken cancellationToken)
    {
        var getImagesByReviewIdQuery = new GetImagesByReviewIdQuery
        {
            ReviewId = reviewId
        };
        var images = await _mediator.Send(getImagesByReviewIdQuery, cancellationToken);
        return images.Select(i => i.Url).ToList();
    }
}
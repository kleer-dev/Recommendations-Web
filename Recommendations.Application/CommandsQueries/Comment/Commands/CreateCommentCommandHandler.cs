using AutoMapper;
using MediatR;
using Recommendations.Application.CommandsQueries.Review.Queries.Get;
using Recommendations.Application.CommandsQueries.User.Queries.Get;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Comment.Commands;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly IMediator _mediator;
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(IMediator mediator,
        IRecommendationsDbContext context, IMapper mapper)
    {
        _mediator = mediator;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Domain.Comment>(request);
        comment.User = await GetUser(request.UserId);
        comment.Review = await GetReview(request.ReviewId);
        
        await _context.Comments.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }

    private async Task<Domain.User> GetUser(Guid? userId)
    {
        var getUserQuery = new GetUserQuery
        {
            UserId = userId
        };
        return await _mediator.Send(getUserQuery);
    }
    
    private async Task<Domain.Review> GetReview(Guid reviewId)
    {
        var getReviewQuery = new GetReviewQuery
        {
            ReviewId = reviewId
        };
        return await _mediator.Send(getReviewQuery);
    }
}
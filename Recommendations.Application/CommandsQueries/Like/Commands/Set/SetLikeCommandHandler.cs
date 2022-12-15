using MediatR;
using Recommendations.Application.CommandsQueries.Like.Commands.Create;
using Recommendations.Application.CommandsQueries.Like.Queries.Get;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.Like.Commands.Set;

public class SetLikeCommandHandler : IRequestHandler<SetLikeCommand, Guid>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMediator _mediator;

    public SetLikeCommandHandler(IRecommendationsDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(SetLikeCommand request,
        CancellationToken cancellationToken)
    {
        var like = await GetLike(request, cancellationToken);
        like.Status = request.IsLike;

        await _context.SaveChangesAsync(cancellationToken);

        return like.Id;
    }

    private async Task<Domain.Like> GetLike(SetLikeCommand request,
        CancellationToken cancellationToken)
    {
        var getLikeQuery = new GetLikeQuery
        {
            ReviewId = request.ReviewId,
            UserId = request.UserId
        };

        var like = await _mediator.Send(getLikeQuery, cancellationToken)
                   ?? await CreateLike(request, cancellationToken);

        return like;
    }

    private async Task<Domain.Like> CreateLike(SetLikeCommand request,
        CancellationToken cancellationToken)
    {
        var createLikeCommand = new CreateLikeCommand
        {
            ReviewId = request.ReviewId,
            UserId = request.UserId,
            isLike = request.IsLike
        };
        var like = await _mediator.Send(createLikeCommand, cancellationToken);
        
        return like;
    }
}
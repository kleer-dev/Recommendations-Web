using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Exceptions;
using Recommendations.Application.Interfaces;

namespace Recommendations.Application.CommandsQueries.Comment.Queries.Get;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, GetCommentDto>
{
    private readonly IRecommendationsDbContext _context;
    private readonly IMapper _mapper;

    public GetCommentQueryHandler(IRecommendationsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetCommentDto> Handle(GetCommentQuery request,
        CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
        if (comment is null)
            throw new NotFoundException(nameof(Comment), request.CommentId);

        return _mapper.Map<GetCommentDto>(comment);
    }
}
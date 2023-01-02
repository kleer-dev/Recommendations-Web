using MediatR;

namespace Recommendations.Application.CommandsQueries.Comment.Queries.Get;

public class GetCommentQuery : IRequest<GetCommentDto>
{
    public Guid CommentId { get; set; }

    public GetCommentQuery(Guid commentId)
    {
        CommentId = commentId;
    }
}
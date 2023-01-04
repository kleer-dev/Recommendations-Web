namespace Recommendations.Application.CommandsQueries.Comment.Queries.GetAll;

public class GetAllCommentsVm
{
    public IEnumerable<GetAllCommentsDto> Comments { get; set; }

    public GetAllCommentsVm(IEnumerable<GetAllCommentsDto> comments)
    {
        Comments = comments;
    }
}
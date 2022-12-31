namespace Recommendations.Application.CommandsQueries.User.Queries.GetAllUsers;

public class GetAllUsersVm
{
    public IEnumerable<GetUserDto> Users { get; set; }
}
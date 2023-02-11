using MediatR;

namespace Recommendations.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQuery : IRequest
{
    public string Email { get; set; }
    public string Password { set; get; }
    public bool Remember { set; get; }
}
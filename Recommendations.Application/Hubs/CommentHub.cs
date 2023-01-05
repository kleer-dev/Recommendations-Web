using MediatR;
using Microsoft.AspNetCore.SignalR;
using Recommendations.Application.CommandsQueries.Comment.Queries.Get;

namespace Recommendations.Application.Hubs;

public class CommentHub : Hub
{
   private readonly IMediator _mediator;

   public CommentHub(IMediator mediator)
   {
      _mediator = mediator;
   }

   public async Task ConnectToGroup(Guid reviewId)
   {
      var connectionId = Context.ConnectionId;
      await Groups.AddToGroupAsync(connectionId, reviewId.ToString());
   }

   public async Task Notify(Guid reviewId, Guid commentId)
   {
      var getCommentQuery = new GetCommentQuery(commentId);
      var comment = await _mediator.Send(getCommentQuery);
      Console.WriteLine(comment.Text);
      await Clients.Group(reviewId.ToString())
         .SendAsync("CommentNotification", comment);
   }
}
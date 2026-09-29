using MediatR;
using PostsCommentsService.Application.Bases;

namespace PostsCommentsService.Application.Feature.Comments.Command.Model
{
    public record DeleteCommentCommand(string CommentId, Guid UserId) : IRequest<Response<string>>
    ;
}

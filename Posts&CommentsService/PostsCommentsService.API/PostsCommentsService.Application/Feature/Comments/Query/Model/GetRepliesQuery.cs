using MediatR;
using PostsCommentsService.Application.Bases;
using PostsCommentsService.Application.Feature.Comments.Query.Results;

namespace PostsCommentsService.Application.Feature.Comments.Query.Model
{
    public record GetRepliesQuery(string ParentCommentId, int Page = 1, int PageSize = 10)
        : IRequest<Response<List<GetRepliesResult>>>;
}
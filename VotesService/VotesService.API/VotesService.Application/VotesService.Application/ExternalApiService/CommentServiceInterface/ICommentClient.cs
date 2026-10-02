using VotesService.Application.dtos;

namespace VotesService.Application.ExternalApiService.CommentServiceInterface
{
    public interface ICommentClient
    {
        Task<GetCommentByIdResult> GetCommentByIdAsync(string PostId, string commentId, CancellationToken cancellationToken = default);
    }
}

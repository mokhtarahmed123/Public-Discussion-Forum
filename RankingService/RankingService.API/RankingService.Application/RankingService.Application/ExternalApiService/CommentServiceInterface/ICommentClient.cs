using RankingService.Application.Models;

namespace RankingService.Application.ExternalApiService.CommentServiceInterface
{
    public interface ICommentClient
    {
        Task<List<CommentSnapshot>> GetCommentsByPostIdAsync(string postId, int page, int pageSize, CancellationToken ct);
    }
}


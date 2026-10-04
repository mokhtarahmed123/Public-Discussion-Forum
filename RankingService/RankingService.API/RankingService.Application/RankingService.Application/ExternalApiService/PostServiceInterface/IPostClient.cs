using RankingService.Application.Models;

namespace RankingService.Application.ExternalApiService.PostServiceInterface
{
    public interface IPostClient
    {
        Task<PostSnapshot> GetpostById(string postId, CancellationToken cancellationToken);
    }
}


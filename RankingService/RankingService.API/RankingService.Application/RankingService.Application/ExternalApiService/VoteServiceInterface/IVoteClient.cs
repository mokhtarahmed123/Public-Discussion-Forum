using RankingService.Application.Models;

namespace RankingService.Application.ExternalApiService.VoteServiceInterface
{
    public interface IVoteClient
    {
        Task<List<TopVoteResult>> GetTopTenVotedCommentsAsync(CancellationToken cancellationToken);
        Task<List<TopVoteResult>> GetTopTenVotedPostsAsync(CancellationToken cancellationToken);
    }
}

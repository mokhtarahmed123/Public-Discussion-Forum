using RankingService.Domain.Events;

namespace RankingService.Application.EventInterface
{
    public interface IPostScoreService
    {
        Task<bool> ApplyDeltaAsync(string messageId, string postId, int delta, CancellationToken ct);
        Task<List<PostScore>> GetTopAsync(int count, CancellationToken ct);
    }
}

using RankingService.Domain.Events;

namespace RankingService.Application.EventInterface
{
    public interface ICommentScoreService
    {
        Task<bool> ApplyDeltaAsync(string eventId, string postId, string commentId, int delta, CancellationToken ct);
        Task<List<CommentScore>> GetTopAsync(string postId, int count, CancellationToken ct);
        Task<Dictionary<string, int>> GetScoresAsync(IEnumerable<string> commentIds, CancellationToken ct);
    }
}

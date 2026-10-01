using VotesService.Domain.Entities;
using VotesService.Domain.Enum;

namespace VotesService.Application.ServiceInterface
{
    public interface IVotesService
    {
        Task<Votes> AddAsync(Votes vote, CancellationToken cancellationToken);

        Task<Votes?> GetUserVoteAsync(Guid userId, VoteTargetType targetType, string targetId, CancellationToken cancellationToken);

        Task UpdateAsync(Votes vote, CancellationToken cancellationToken);

        Task DeleteAsync(Votes vote, CancellationToken cancellationToken);
        Task SetLockedAsync(VoteTargetType targetType, string targetId, bool locked, CancellationToken cancellationToken);
        Task<bool> IsLockedAsync(VoteTargetType targetType, string targetId, CancellationToken cancellationToken);
        Task<VoteSummary> GetSummaryAsync(VoteTargetType targetType, string targetId, CancellationToken cancellationToken);

        Task<List<Votes>> GetUserVotesAsync(Guid userId, VoteTargetType targetType, IEnumerable<string> targetIds, CancellationToken cancellationToken);
    }
    public record VoteSummary(int UpVotes, int DownVotes)
    {
        public int Score => UpVotes - DownVotes;
    }
}

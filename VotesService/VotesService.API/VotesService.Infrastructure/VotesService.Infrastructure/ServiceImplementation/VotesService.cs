using MongoDB.Driver.Linq;
using VotesService.Application.RepositoryInterface;
using VotesService.Application.ServiceInterface;
using VotesService.Domain.Entities;
using VotesService.Domain.Enum;

namespace VotesService.Infrastructure.ServiceImplementation
{
    public class VotesService : IVotesService
    {
        private readonly IVotesRepository votesRepository;

        public VotesService(IVotesRepository votesRepository)
        {
            this.votesRepository = votesRepository;
        }
        public async Task<Votes> AddAsync(Votes vote, CancellationToken cancellationToken)
        {
            await votesRepository.AddAsync(vote, cancellationToken);
            return vote;

        }

        public async Task DeleteAsync(Votes vote, CancellationToken cancellationToken)
        {

            await votesRepository.DeleteAsync(vote, cancellationToken);
        }

        public async Task<VoteSummary> GetSummaryAsync(VoteTargetType targetType, string targetId, CancellationToken cancellationToken)
        {
            var query = votesRepository.GetTableNoTracking(cancellationToken)
        .Where(v => v.TargetType == targetType && v.TargetId == targetId);

            var up = await query.CountAsync(v => v.Type == VoteType.Up, cancellationToken);
            var down = await query.CountAsync(v => v.Type == VoteType.Down, cancellationToken);

            return new VoteSummary(up, down);
        }

        public async Task<Votes?> GetUserVoteAsync(Guid userId, VoteTargetType targetType, string targetId, CancellationToken cancellationToken)
        {
            return await votesRepository.GetTableNoTracking(cancellationToken)
                .FirstOrDefaultAsync(v => v.UserId == userId && v.TargetType == targetType && v.TargetId == targetId, cancellationToken);

        }

        public Task<List<Votes>> GetUserVotesAsync(Guid userId, VoteTargetType targetType, IEnumerable<string> targetIds, CancellationToken cancellationToken)
        {
            return votesRepository.GetTableNoTracking(cancellationToken)
                .Where(v => v.UserId == userId && v.TargetType == targetType && targetIds.Contains(v.TargetId))
                .ToListAsync(cancellationToken);

        }

        public async Task<bool> IsLockedAsync(VoteTargetType targetType, string targetId, CancellationToken cancellationToken)
        {
            return await votesRepository.GetTableNoTracking(cancellationToken)
      .AnyAsync(v => v.TargetType == targetType && v.TargetId == targetId && v.Locked, cancellationToken);

        }

        public async Task SetLockedAsync(VoteTargetType targetType, string targetId, bool locked, CancellationToken cancellationToken)
        {
            var votes = await votesRepository.GetTableNoTracking(cancellationToken)
             .Where(v => v.TargetType == targetType && v.TargetId == targetId && v.Locked != locked)
             .ToListAsync(cancellationToken);

            if (votes.Count == 0) return;

            foreach (var vote in votes)
                vote.Locked = locked;

            await votesRepository.UpdateRangeAsync(votes, cancellationToken);
        }

        public Task UpdateAsync(Votes vote, CancellationToken cancellationToken)
        {
            return votesRepository.UpdateAsync(vote, cancellationToken);

        }
    }
}

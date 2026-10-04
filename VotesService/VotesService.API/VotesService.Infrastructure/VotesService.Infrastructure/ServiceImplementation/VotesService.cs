using MassTransit;
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
        private readonly IPublishEndpoint publishEndpoint;

        public VotesService(IVotesRepository votesRepository, IPublishEndpoint publishEndpoint)
        {
            this.votesRepository = votesRepository;
            this.publishEndpoint = publishEndpoint;
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
            var result = await votesRepository.GetTableNoTracking(cancellationToken)
                .Where(v => v.TargetType == targetType && v.TargetId == targetId)
                .GroupBy(_ => 1)
                .Select(g => new
                {
                    Up = g.Count(v => v.Type == VoteType.Up),
                    Down = g.Count(v => v.Type == VoteType.Down)
                })
                .FirstOrDefaultAsync(cancellationToken);

            return new VoteSummary(result?.Up ?? 0, result?.Down ?? 0);
        }

        public async Task<List<TopVoteResult>> GetTopTenAsync(VoteTargetType targetType, CancellationToken cancellationToken)
        {
            var rows = await votesRepository.GetTableNoTracking(cancellationToken)
         .Where(v => v.TargetType == targetType)
         .GroupBy(v => v.TargetId)
         .Select(g => new
         {
             TargetId = g.Key,
             PostId = g.Max(v => v.PostId),
             Score = g.Sum(v => v.Type == VoteType.Up ? 1 : -1)
         })
         .OrderByDescending(x => x.Score)
         .ThenBy(x => x.TargetId)
         .Take(10)
         .ToListAsync(cancellationToken);

            return rows
                .Select(x =>
                {
                    if (targetType == VoteTargetType.Post)
                    {
                        return new TopVoteResult(
                            TargetId: x.TargetId,
                            TargetType: targetType,
                            Score: x.Score,
                            PostId: x.TargetId,
                            CommentId: null);
                    }

                    return new TopVoteResult(
                        TargetId: x.TargetId,
                        TargetType: targetType,
                        Score: x.Score,
                        PostId: x.PostId,
                        CommentId: x.TargetId);
                })
                .ToList();
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

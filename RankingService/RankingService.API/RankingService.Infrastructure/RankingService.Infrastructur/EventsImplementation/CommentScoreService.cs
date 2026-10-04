using MongoDB.Driver;
using RankingService.Application.EventInterface;
using RankingService.Domain.Entities;
using RankingService.Domain.Events;
using RankingService.Infrastructure.DataBaseConfiguration;

public class CommentScoreService : ICommentScoreService
{
    private readonly ForumService forum;

    public CommentScoreService(ForumService forum) => this.forum = forum;

    public async Task<bool> ApplyDeltaAsync(
        string eventId, string postId, string commentId, int delta, CancellationToken ct)
    {
        try
        {
            await forum.ProcessedMessages.InsertOneAsync(
                new ProcessedEvent { EventId = eventId, ProcessedAt = DateTime.UtcNow },
                cancellationToken: ct);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            return false;
        }

        await forum.CommentScores.UpdateOneAsync(
            Builders<CommentScore>.Filter.Eq(x => x.CommentId, commentId),
            Builders<CommentScore>.Update
                .Inc(x => x.Score, delta)
                .Set(x => x.PostId, postId)
                .Set(x => x.UpdatedAt, DateTime.UtcNow),
            new UpdateOptions { IsUpsert = true },
            ct);

        return true;
    }

    public async Task<List<CommentScore>> GetTopAsync(string postId, int count, CancellationToken ct) =>
        await forum.CommentScores
            .Find(x => x.PostId == postId)
            .SortByDescending(x => x.Score)
            .Limit(count)
            .ToListAsync(ct);

    public async Task<Dictionary<string, int>> GetScoresAsync(
        IEnumerable<string> commentIds, CancellationToken ct)
    {
        var ids = commentIds.Distinct().ToList();

        var scores = await forum.CommentScores
            .Find(Builders<CommentScore>.Filter.In(x => x.CommentId, ids))
            .ToListAsync(ct);

        return scores.ToDictionary(x => x.CommentId, x => x.Score);
    }
}
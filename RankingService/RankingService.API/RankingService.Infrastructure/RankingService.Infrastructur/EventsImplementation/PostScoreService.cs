using MongoDB.Driver;
using RankingService.Application.EventInterface;
using RankingService.Domain.Entities;
using RankingService.Domain.Events;
using RankingService.Infrastructure.DataBaseConfiguration;

public class PostScoreService : IPostScoreService
{
    private readonly ForumService forum;

    public PostScoreService(ForumService forum) => this.forum = forum;

    public async Task<bool> ApplyDeltaAsync(string messageId, string postId, int delta, CancellationToken ct)
    {
        try
        {
            await forum.ProcessedMessages.InsertOneAsync(
                new ProcessedEvent { EventId = messageId, ProcessedAt = DateTime.UtcNow },
                cancellationToken: ct);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            return false;
        }

        await forum.PostScores.UpdateOneAsync(
            Builders<PostScore>.Filter.Eq(x => x.PostId, postId),
            Builders<PostScore>.Update
                .Inc(x => x.Score, delta)
                .Set(x => x.UpdatedAt, DateTime.UtcNow),
            new UpdateOptions { IsUpsert = true },
            ct);

        return true;
    }

    public async Task<List<PostScore>> GetTopAsync(int count, CancellationToken ct) =>
        await forum.PostScores.Find(_ => true)
            .SortByDescending(x => x.Score)
            .Limit(count)
            .ToListAsync(ct);
}
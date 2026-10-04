using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RankingService.Application.Bases;
using RankingService.Domain.Entities;
using RankingService.Domain.Events;

namespace RankingService.Infrastructure.DataBaseConfiguration
{
    public class ForumService
    {
        public IMongoCollection<ProcessedEvent> ProcessedMessages { get; }
        public IMongoCollection<PostScore> PostScores { get; }
        public IMongoCollection<CommentScore> CommentScores { get; }

        private readonly IMongoDatabase _database;
        private readonly RankingDatabaseSettings _settings;

        public ForumService(IOptions<RankingDatabaseSettings> options)
        {
            _settings = options.Value;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);

            ProcessedMessages = _database.GetCollection<ProcessedEvent>(_settings.ProcessedEventsCollectionName);
            PostScores = _database.GetCollection<PostScore>(_settings.PostScoresCollectionName);
            CommentScores = _database.GetCollection<CommentScore>(_settings.CommentScoresCollectionName);
            CreateIndexes();
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var name = typeof(T).Name switch
            {
                nameof(TopTenComments) => _settings.CommentsCollectionName,
                nameof(TopTenPosts) => _settings.PostsCollectionName,
                nameof(ProcessedEvent) => _settings.ProcessedEventsCollectionName,
                nameof(PostScore) => _settings.PostScoresCollectionName,
                _ => throw new InvalidOperationException($"No collection configured for {typeof(T).Name}")
            };
            return _database.GetCollection<T>(name);
        }

        private void CreateIndexes()
        {

            ProcessedMessages.Indexes.CreateOne(new CreateIndexModel<ProcessedEvent>(
                Builders<ProcessedEvent>.IndexKeys.Ascending(x => x.ProcessedAt),
                new CreateIndexOptions { ExpireAfter = TimeSpan.FromDays(1) }));
        }
    }
}
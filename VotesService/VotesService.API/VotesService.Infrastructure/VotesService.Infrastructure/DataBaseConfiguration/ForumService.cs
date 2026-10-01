using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VotesService.Application.Bases;
using VotesService.Domain.Entities;

namespace VotesService.Infrastructure.DataBaseConfiguration
{
    public class ForumService
    {
        private readonly IMongoDatabase _database;
        private readonly VotesDatabaseSettings _settings;
        public ForumService(IOptions<VotesDatabaseSettings> options)
        {
            _settings = options.Value;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }
        public IMongoCollection<T> GetCollection<T>()
        {
            var name = typeof(T).Name switch
            {
                nameof(Votes) => _settings.VotesCollectionName,
                _ => throw new InvalidOperationException($"No collection configured for {typeof(T).Name}")
            };
            return _database.GetCollection<T>(name);
        }
    }
}

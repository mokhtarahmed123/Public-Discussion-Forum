using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using VotesService.Domain.Entities;

namespace VotesService.Infrastructure.DataBaseConfiguration
{
    public class VotesIndexInitializer : IHostedService
    {
        private readonly IMongoCollection<Votes> _collection;


        public VotesIndexInitializer(ForumService votesDatabase)
        {
            _collection = votesDatabase.GetCollection<Votes>();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var uniqueVote = new CreateIndexModel<Votes>(
             Builders<Votes>.IndexKeys
                 .Ascending(v => v.UserId)
                 .Ascending(v => v.TargetType)
                 .Ascending(v => v.TargetId),
             new CreateIndexOptions { Unique = true, Name = "ux_user_target" });

            var byTarget = new CreateIndexModel<Votes>(
                Builders<Votes>.IndexKeys
                    .Ascending(v => v.TargetType)
                    .Ascending(v => v.TargetId),
                new CreateIndexOptions { Name = "ix_target" });

            await _collection.Indexes.CreateManyAsync(new[] { uniqueVote, byTarget }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}

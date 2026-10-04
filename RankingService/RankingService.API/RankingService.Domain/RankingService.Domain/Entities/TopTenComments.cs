using MongoDB.Bson.Serialization.Attributes;
namespace RankingService.Domain.Entities
{
    public class TopTenComments
    {
        [BsonId]
        public string Id { get; init; } = default!;
        public string PostId { get; init; } = default!;
        public string Content { get; init; } = default!;
        public int Score { get; set; }
        public int Rank { get; set; }
        public int LikesCount { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}

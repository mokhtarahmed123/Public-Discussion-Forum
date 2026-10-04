using MongoDB.Bson.Serialization.Attributes;

namespace RankingService.Domain.Events
{
    public class CommentScore
    {
        [BsonId]
        public string CommentId { get; set; } = default!;
        public int Score { get; set; }
        public string PostId { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

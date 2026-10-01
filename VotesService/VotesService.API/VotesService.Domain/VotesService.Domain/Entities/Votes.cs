using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using VotesService.Domain.Enum;

namespace VotesService.Domain.Entities
{
    public class Votes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string TargetId { get; set; } = null!;

        [BsonRepresentation(BsonType.String)]
        public VoteTargetType TargetType { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public VoteType Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool Locked { get; set; } = false;
    }
}

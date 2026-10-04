using MongoDB.Bson.Serialization.Attributes;

namespace RankingService.Domain.Events
{
    public class ProcessedEvent
    {
        [BsonId]
        public string EventId { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}

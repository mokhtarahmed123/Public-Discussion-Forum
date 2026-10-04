namespace Forum.Contracts
{
    public record VoteCommentMessage
    {
        public Guid EventId { get; init; } = Guid.NewGuid(); // للـ idempotency
        public string CommentId { get; init; }
        public string PostId { get; init; }
        public Guid UserId { get; init; }
        public int Delta { get; init; }   // +1, -1, +2, -2
        public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    }
}

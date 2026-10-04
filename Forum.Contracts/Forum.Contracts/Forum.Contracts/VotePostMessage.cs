namespace Forum.Contracts
{
    public record VotePostMessage
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public string PostId { get; init; }
        public Guid UserId { get; init; }
        public int Delta { get; init; }
        public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    }
}

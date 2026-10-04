namespace VotesService.Application.dtos
{
    public record VoteCommentMessage
    {
        public Guid EventId { get; init; } = Guid.NewGuid(); // للـ idempotency
        public Guid CommentId { get; init; }
        public Guid UserId { get; init; }
        public int Delta { get; init; }   // +1, -1, +2, -2
        public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    }
}

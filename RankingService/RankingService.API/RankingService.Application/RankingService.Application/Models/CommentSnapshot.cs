namespace RankingService.Application.Models
{
    public class CommentSnapshot
    {
        public string Id { get; init; } = default!;
        public string PostId { get; init; } = default!;
        public string? ParentCommentId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; } = default!;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public bool IsEdited { get; init; }
        public int Rank { get; set; }
        public int LikesCount { get; init; }
        public int RepliesCount { get; init; }
        public bool IsDeleted { get; init; }
        public DateTime? DeletedAt { get; init; }
    }
}

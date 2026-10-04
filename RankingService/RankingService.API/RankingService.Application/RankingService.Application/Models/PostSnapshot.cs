namespace RankingService.Application.Models
{

    public record PostSnapshot
    {
        public string id { get; init; } = default!;
        public string title { get; init; } = default!;
        public string content { get; init; } = default!;
        public int typeOfPosts { get; init; }          // كان string
        public Guid userId { get; init; }
        public DateTime createdAt { get; init; }
        public DateTime? updatedAt { get; init; }
        public int likesCount { get; init; }
        public int commentsCount { get; init; }
        public bool isDeleted { get; init; }
        public bool isPinned { get; init; }
        public bool isLocked { get; init; }
        public DateTime? deletedAt { get; init; }


    }

}


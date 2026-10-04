namespace RankingService.Application.Models
{
    public record TopVoteResult(string TargetId, int TargetType, int Score, string? PostId, string? CommentId);
}

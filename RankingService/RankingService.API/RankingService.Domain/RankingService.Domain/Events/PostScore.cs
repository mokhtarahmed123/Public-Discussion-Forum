namespace RankingService.Domain.Events
{
    public class PostScore
    {
        public string PostId { get; set; } = default!;
        public long Score { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

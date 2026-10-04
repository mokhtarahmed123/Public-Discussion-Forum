namespace RankingService.Application.Bases
{
    public class RankingDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CommentsCollectionName { get; set; } = null!;
        public string PostsCollectionName { get; set; } = null!;

        public string ProcessedEventsCollectionName { get; set; } = "ProcessedEvents";
        public string PostScoresCollectionName { get; set; } = "PostScores";
        public string CommentScoresCollectionName { get; set; } = "CommentScores";
    }
}

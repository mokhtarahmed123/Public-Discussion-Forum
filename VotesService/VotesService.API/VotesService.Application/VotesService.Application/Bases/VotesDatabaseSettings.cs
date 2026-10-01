namespace VotesService.Application.Bases
{
    public class VotesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string VotesCollectionName { get; set; } = null!;

    }
}

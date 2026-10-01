namespace VotesService.Application.Feature.Votes.Query.Results
{
    public class GetVoteSummaryResult
    {
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public int Score { get; set; }
    }
}

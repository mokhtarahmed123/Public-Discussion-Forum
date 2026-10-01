using MediatR;
using MongoDB.Bson;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Query.Model;
using VotesService.Application.Feature.Votes.Query.Results;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Query.Handler
{
    public class GetVoteSummaryQueryHandler : ResponseHandler,
        IRequestHandler<GetVoteSummaryQuery, Response<GetVoteSummaryResult>>
    {
        private readonly IVotesService votesService;

        public GetVoteSummaryQueryHandler(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        public async Task<Response<GetVoteSummaryResult>> Handle(
            GetVoteSummaryQuery request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.TargetId, out _))
                return BadRequest<GetVoteSummaryResult>("رقم العنصر غير صحيح.");

            var summary = await votesService.GetSummaryAsync(
                request.TargetType, request.TargetId, cancellationToken);

            var result = new GetVoteSummaryResult
            {
                UpVotes = summary.UpVotes,
                DownVotes = summary.DownVotes,
                Score = summary.Score
            };

            return Success(result);
        }
    }
}
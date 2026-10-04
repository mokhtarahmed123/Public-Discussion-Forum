using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Query.Model;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Query.Handler
{
    public class GetTopTenPostHandler : ResponseHandler, IRequestHandler<GetTopTenPostAsync, Response<List<TopVoteResult>>>
    {
        private readonly IVotesService votesService;

        public GetTopTenPostHandler(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        public async Task<Response<List<TopVoteResult>>> Handle(GetTopTenPostAsync request, CancellationToken cancellationToken)
        {
            var result = await votesService.GetTopTenAsync(Domain.Enum.VoteTargetType.Post, cancellationToken);
            return Success(result);

        }
    }
}

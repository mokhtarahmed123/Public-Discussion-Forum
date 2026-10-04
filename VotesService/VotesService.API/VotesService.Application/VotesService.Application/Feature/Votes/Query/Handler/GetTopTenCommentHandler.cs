using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Query.Model;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Query.Handler
{
    public class GetTopTenCommentHandler : ResponseHandler, IRequestHandler<GetTopTenCommentAsync, Response<List<TopVoteResult>>>
    {
        private readonly IVotesService votesService;

        public GetTopTenCommentHandler(IVotesService votesService)
        {
            this.votesService = votesService;
        }
        public async Task<Response<List<TopVoteResult>>> Handle(GetTopTenCommentAsync request, CancellationToken cancellationToken)
        {
            var result = await votesService.GetTopTenAsync(Domain.Enum.VoteTargetType.Comment, cancellationToken);
            return Success(result);


        }
    }
}

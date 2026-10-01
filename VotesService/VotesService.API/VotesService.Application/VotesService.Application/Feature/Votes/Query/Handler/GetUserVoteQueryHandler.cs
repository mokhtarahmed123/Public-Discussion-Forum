using MediatR;
using MongoDB.Bson;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Query.Model;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Query.Handler
{
    public class GetUserVoteQueryHandler : ResponseHandler,
        IRequestHandler<GetUserVoteQuery, Response<GetUserVoteResult>>
    {
        private readonly IVotesService votesService;

        public GetUserVoteQueryHandler(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        public async Task<Response<GetUserVoteResult>> Handle(
            GetUserVoteQuery request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.TargetId, out _))
                return BadRequest<GetUserVoteResult>("رقم العنصر غير صحيح.");

            if (request.UserId == Guid.Empty)
                return BadRequest<GetUserVoteResult>("المستخدم غير معروف.");

            var vote = await votesService.GetUserVoteAsync(
                request.UserId, request.TargetType, request.TargetId, cancellationToken);

            return Success(new GetUserVoteResult { Type = vote?.Type });
        }
    }
}
using MediatR;
using MongoDB.Bson;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Command.Model;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Command.Handler
{
    public class LockVoteCommandHandler : ResponseHandler, IRequestHandler<LockVoteCommand, Response<bool>>
    {
        private readonly IVotesService votesService;

        public LockVoteCommandHandler(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        public async Task<Response<bool>> Handle(LockVoteCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.TargetId, out _))
                return BadRequest<bool>("رقم العنصر غير صحيح.");

            // TODO: Check if the user has permission to lock/unlock the vote for the target item.

            await votesService.SetLockedAsync(
                request.TargetType, request.TargetId, request.IsLocked, cancellationToken);

            return Success(request.IsLocked);
        }
    }
}
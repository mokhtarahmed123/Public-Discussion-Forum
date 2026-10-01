using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Command.Model;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Command.Handler
{
    public class VoteCommandHandler : ResponseHandler, IRequestHandler<VoteCommand, Response<string>>
    {
        private readonly IVotesService votesService;

        public VoteCommandHandler(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        public async Task<Response<string>> Handle(Model.VoteCommand request, CancellationToken cancellationToken)
        {
            var existing = await votesService.GetUserVoteAsync(
             request.UserId, request.TargetType, request.TargetId, cancellationToken);


            if (existing is null)
            {
                var vote = new Domain.Entities.Votes
                {
                    TargetId = request.TargetId,
                    TargetType = request.TargetType,
                    UserId = request.UserId,
                    Type = request.Type
                };

                var created = await votesService.AddAsync(vote, cancellationToken);
                return Success(created.Id);
            }
            if (existing.Type == request.Type)
            {
                await votesService.DeleteAsync(existing, cancellationToken);
                return Deleted<string>();
            }
            existing.Type = request.Type;
            existing.UpdatedAt = DateTime.UtcNow;
            await votesService.UpdateAsync(existing, cancellationToken);
            return Success(existing.Id);

        }
    }
}

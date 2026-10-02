using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.ExternalApiService.PostServiceInterface;
using VotesService.Application.Feature.Votes.Command.Model;
using VotesService.Application.ServiceInterface;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Command.Handler
{
    public class VotePostCommandHandler : ResponseHandler, IRequestHandler<VotePostCommand, Response<string>>
    {
        private readonly IVotesService votesService;
        private readonly IPostClient postClient;

        public VotePostCommandHandler(IVotesService votesService, IPostClient postClient)
        {
            this.votesService = votesService;
            this.postClient = postClient;
        }

        public async Task<Response<string>> Handle(VotePostCommand request, CancellationToken cancellationToken)
        {
            var existing = await votesService.GetUserVoteAsync(
                request.UserId, VoteTargetType.Post, request.TargetId, cancellationToken);


            if (existing is not null && existing.Locked)
                return BadRequest<string>("تصويتك مقفول ومينفعش يتغير.");

            if (existing is null &&
                await votesService.IsLockedAsync(VoteTargetType.Post, request.TargetId, cancellationToken))
                return BadRequest<string>("التصويت مقفول على البوست ده.");


            if (existing is null)
            {

                var post = await postClient.GetPostByIdAsync(request.TargetId, cancellationToken);
                if (post is null || post.IsDeleted)
                    return NotFound<string>("البوست مش موجود.");

                var vote = new Domain.Entities.Votes
                {
                    TargetId = request.TargetId,
                    PostId = request.TargetId,
                    TargetType = VoteTargetType.Post,
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
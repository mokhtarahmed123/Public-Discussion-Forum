using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.ExternalApiService.CommentServiceInterface;
using VotesService.Application.Feature.Votes.Command.Model;
using VotesService.Application.ServiceInterface;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Command.Handler
{
    public class VoteCommentCommandHandler : ResponseHandler, IRequestHandler<VoteCommentCommand, Response<string>>
    {
        private readonly IVotesService votesService;
        private readonly ICommentClient commentClient;

        public VoteCommentCommandHandler(IVotesService votesService, ICommentClient commentClient)
        {
            this.votesService = votesService;
            this.commentClient = commentClient;
        }

        public async Task<Response<string>> Handle(VoteCommentCommand request, CancellationToken cancellationToken)
        {
            var existing = await votesService.GetUserVoteAsync(
                request.UserId, VoteTargetType.Comment, request.TargetId, cancellationToken);

            // 1) القفل: لو مقفول مفيش تغيير ولا إلغاء ولا تصويت جديد
            if (existing is not null && existing.Locked)
                return BadRequest<string>("تصويتك مقفول ومينفعش يتغير.");

            if (existing is null &&
                await votesService.IsLockedAsync(VoteTargetType.Comment, request.TargetId, cancellationToken))
                return BadRequest<string>("التصويت مقفول على الكومنت ده.");

            // 2) تصويت جديد
            if (existing is null)
            {
                // الكومنت لازم يكون موجود (call خارجي، فبنعمله بس لما هنضيف تصويت جديد)
                var comment = await commentClient.GetCommentByIdAsync(request.PostId, request.TargetId, cancellationToken);
                if (comment is null || comment.IsDeleted)
                    return NotFound<string>("الكومنت مش موجود.");

                var vote = new Domain.Entities.Votes
                {
                    TargetId = request.TargetId,
                    TargetType = VoteTargetType.Comment,
                    UserId = request.UserId,
                    PostId = request.PostId,
                    Type = request.Type
                };

                var created = await votesService.AddAsync(vote, cancellationToken);
                return Success(created.Id);
            }

            // 3) نفس النوع تاني: إلغاء التصويت
            if (existing.Type == request.Type)
            {
                await votesService.DeleteAsync(existing, cancellationToken);
                return Deleted<string>();
            }

            // 4) نوع مختلف: تغيير التصويت
            existing.Type = request.Type;
            existing.UpdatedAt = DateTime.UtcNow;
            await votesService.UpdateAsync(existing, cancellationToken);

            return Success(existing.Id);
        }
    }
}
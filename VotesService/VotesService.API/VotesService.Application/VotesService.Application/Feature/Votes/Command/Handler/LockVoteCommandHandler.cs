using MediatR;
using MongoDB.Bson;
using VotesService.Application.Bases;
using VotesService.Application.ExternalApiService.CommentServiceInterface;
using VotesService.Application.ExternalApiService.PostServiceInterface;
using VotesService.Application.Feature.Votes.Command.Model;
using VotesService.Application.ServiceInterface;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Command.Handler
{
    public class LockVoteCommandHandler : ResponseHandler, IRequestHandler<LockVoteCommand, Response<bool>>
    {
        private readonly IVotesService votesService;
        private readonly IPostClient postClient;
        private readonly ICommentClient commentClient;

        public LockVoteCommandHandler(IVotesService votesService, IPostClient postClient,
            ICommentClient commentClient)
        {
            this.votesService = votesService;
            this.postClient = postClient;
            this.commentClient = commentClient;
        }

        public async Task<Response<bool>> Handle(LockVoteCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.TargetId, out _))
                return BadRequest<bool>("رقم العنصر غير صحيح.");


            Guid ownerId;

            if (request.TargetType == VoteTargetType.Post)
            {
                var post = await postClient.GetPostByIdAsync(request.TargetId, cancellationToken);

                if (post is null || post.IsDeleted)
                    return NotFound<bool>("البوست مش موجود.");

                ownerId = post.UserId;
            }
            else
            {
                if (!ObjectId.TryParse(request.TargetId, out _))
                    return BadRequest<bool>("رقم البوست غير صحيح.");

                var comment = await commentClient.GetCommentByIdAsync(request.PostId, request.TargetId, cancellationToken);

                if (comment is null || comment.IsDeleted)
                    return NotFound<bool>("الكومنت مش موجود.");

                ownerId = comment.UserId;
            }


            if (ownerId != request.UserId)
                return Forbidden<bool>("مش مسموحلك تقفل أو تفتح التصويت على العنصر ده.");

            // 3) القفل
            await votesService.SetLockedAsync(
                request.TargetType, request.TargetId, request.IsLocked, cancellationToken);

            return Success(request.IsLocked);
        }
    }
}
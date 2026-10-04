using Forum.Contracts;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RankingService.Application.EventInterface;
using RankingService.Application.Feature.Comments.Command.Model;

namespace RankingService.Application.Consumers
{
    public class VoteCommentConsumer : IConsumer<VoteCommentMessage>
    {
        private readonly ICommentScoreService commentScoreService;
        private readonly IMediator mediator;
        private readonly ILogger<VoteCommentConsumer> logger;

        public VoteCommentConsumer(
            ICommentScoreService commentScoreService,
            IMediator mediator,
            ILogger<VoteCommentConsumer> logger)
        {
            this.commentScoreService = commentScoreService;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<VoteCommentMessage> context)
        {
            var msg = context.Message;
            var ct = context.CancellationToken;

            var applied = await commentScoreService.ApplyDeltaAsync(
                msg.EventId.ToString(), msg.PostId, msg.CommentId, msg.Delta, ct);

            if (!applied)
            {
                logger.LogInformation("Duplicate event {EventId} ignored.", msg.EventId);
                return;
            }

            try
            {
                var result = await mediator.Send(new AddTopTenCommentByPostIdCommand(msg.PostId), ct);
                logger.LogInformation("Top comments refreshed for post {PostId}: {Message}", msg.PostId, result.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Top comments refresh failed for post {PostId}", msg.PostId);
            }
        }
    }
}
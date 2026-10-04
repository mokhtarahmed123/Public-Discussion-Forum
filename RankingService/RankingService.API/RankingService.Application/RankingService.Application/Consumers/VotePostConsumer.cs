using Forum.Contracts;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RankingService.Application.EventInterface;
using RankingService.Application.Feature.Posts.Command.Model;

namespace RankingService.Application.Consumers
{
    public class VotePostConsumer : IConsumer<VotePostMessage>
    {
        private readonly IPostScoreService postScoreService;
        private readonly IMediator mediator;
        private readonly ILogger<VotePostConsumer> logger;

        public VotePostConsumer(
            IPostScoreService postScoreService,
            IMediator mediator,
            ILogger<VotePostConsumer> logger)
        {
            this.postScoreService = postScoreService;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<VotePostMessage> context)
        {
            var msg = context.Message;
            var ct = context.CancellationToken;

            var applied = await postScoreService.ApplyDeltaAsync(
                msg.EventId.ToString(), msg.PostId, msg.Delta, ct);

            if (!applied)
            {
                logger.LogInformation("Duplicate event {EventId} ignored.", msg.EventId);
                return;
            }

            try
            {
                var result = await mediator.Send(new AddTopTenPostsCommand(), ct);
                logger.LogInformation("Top ten refreshed after vote: {Message}", result.Message);
            }
            catch (Exception ex)
            {
                // الـ score اتحدّث خلاص، فمنفشّلش الرسالة لو الـ refresh فشل
                logger.LogError(ex, "Top ten refresh failed after vote");
            }
        }
    }
}
using AutoMapper;
using MediatR;
using RankingService.Application.Bases;
using RankingService.Application.EventInterface;
using RankingService.Application.ExternalApiService.PostServiceInterface;
using RankingService.Application.Feature.Posts.Command.Model;
using RankingService.Application.ServiceInterface;
using RankingService.Domain.Entities;

namespace RankingService.Application.Feature.Posts.Command.Handler
{
    public class AddTopTenPostsCommandHandler : ResponseHandler, IRequestHandler<AddTopTenPostsCommand, Response<string>>
    {
        private readonly IPostScoreService postScoreService;
        private readonly IPostClient postClient;
        private readonly ITopTenPostsService topTenPostsService;
        private readonly IMapper mapper;

        public AddTopTenPostsCommandHandler(
            IPostScoreService postScoreService,
            IPostClient postClient,
            ITopTenPostsService topTenPostsService,
            IMapper mapper)
        {
            this.postScoreService = postScoreService;
            this.postClient = postClient;
            this.topTenPostsService = topTenPostsService;
            this.mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddTopTenPostsCommand request, CancellationToken cancellationToken)
        {

            var scores = await postScoreService.GetTopAsync(20, cancellationToken);

            if (scores is null || scores.Count == 0)
                return BadRequest<string>("مفيش بوستات متصوت عليها.");

            var topTen = new List<TopTenPosts>();

            foreach (var s in scores)
            {
                if (topTen.Count == 10)
                    break;

                var post = await postClient.GetpostById(s.PostId, cancellationToken);

                if (post is null || post.isDeleted)
                    continue;

                var item = mapper.Map<TopTenPosts>(post);
                item.Score = (int)s.Score;
                topTen.Add(item);
            }

            if (topTen.Count == 0)
                return NotFound<string>("البوستات مش موجودة.");

            await topTenPostsService.ReplaceAll(topTen, cancellationToken);

            return Success("تم تحديث أفضل 10 بوستات.");
        }
    }
}
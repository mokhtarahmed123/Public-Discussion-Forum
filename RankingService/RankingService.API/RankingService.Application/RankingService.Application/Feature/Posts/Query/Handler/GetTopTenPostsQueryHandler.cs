using MediatR;
using RankingService.Application.Bases;
using RankingService.Application.Feature.Posts.Query.Model;
using RankingService.Application.ServiceInterface;
using RankingService.Domain.Entities;

namespace RankingService.Application.Feature.Posts.Query.Handler
{
    public class GetTopTenPostsQueryHandler : ResponseHandler, IRequestHandler<GetTopTenPostsQuery, Response<List<TopTenPosts>>>
    {
        private readonly ITopTenPostsService topTenPostsService;

        public GetTopTenPostsQueryHandler(ITopTenPostsService topTenPostsService)
        {
            this.topTenPostsService = topTenPostsService;
        }

        public async Task<Response<List<TopTenPosts>>> Handle(
            GetTopTenPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await topTenPostsService.GetAll(cancellationToken);

            if (posts is null || posts.Count == 0)
                return NotFound<List<TopTenPosts>>("مفيش بوستات في الـ top ten لسه.");

            var ordered = posts
                .OrderByDescending(p => p.Score)
                .Take(10)
                .ToList();

            return Success(ordered);
        }
    }
}

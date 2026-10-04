using MediatR;
using RankingService.Application.Bases;
using RankingService.Domain.Entities;

namespace RankingService.Application.Feature.Posts.Query.Model
{
    public record GetTopTenPostsQuery : IRequest<Response<List<TopTenPosts>>>
;
}

using MediatR;
using RankingService.Application.Bases;

namespace RankingService.Application.Feature.Posts.Command.Model
{
    public record AddTopTenPostsCommand : IRequest<Response<string>>
    ;
}

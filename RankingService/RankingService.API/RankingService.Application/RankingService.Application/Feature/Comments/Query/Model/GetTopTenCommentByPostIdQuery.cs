using MediatR;
using RankingService.Application.Bases;
using RankingService.Domain.Entities;

namespace RankingService.Application.Feature.Comments.Query.Model
{
    public record GetTopTenCommentByPostIdQuery(string PostId)
          : IRequest<Response<List<TopTenComments>>>;
}

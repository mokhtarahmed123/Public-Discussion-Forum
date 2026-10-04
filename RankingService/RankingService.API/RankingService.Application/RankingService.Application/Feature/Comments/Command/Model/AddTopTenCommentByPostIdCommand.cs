using MediatR;
using RankingService.Application.Bases;

namespace RankingService.Application.Feature.Comments.Command.Model
{
    public record AddTopTenCommentByPostIdCommand(string PostId, int PageNumber = 1, int PageSize = 10)
           : IRequest<Response<string>>;

}

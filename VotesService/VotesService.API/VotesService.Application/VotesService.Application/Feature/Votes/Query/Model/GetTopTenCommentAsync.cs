using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.ServiceInterface;

namespace VotesService.Application.Feature.Votes.Query.Model
{
    public record GetTopTenCommentAsync : IRequest<Response<List<TopVoteResult>>>
   ;
}

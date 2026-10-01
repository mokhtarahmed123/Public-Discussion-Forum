using MediatR;
using VotesService.Application.Bases;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Query.Model
{
    public record GetUserVoteQuery(VoteTargetType TargetType, string TargetId, Guid UserId)
        : IRequest<Response<GetUserVoteResult>>;
}
public record GetUserVoteResult
{
    public VoteType? Type { get; set; }
}

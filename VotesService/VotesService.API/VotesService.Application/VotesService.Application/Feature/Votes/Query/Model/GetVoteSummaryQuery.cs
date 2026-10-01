using MediatR;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Query.Results;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Query.Model
{
    public record GetVoteSummaryQuery(VoteTargetType TargetType, string TargetId)
        : IRequest<Response<GetVoteSummaryResult>>;

}

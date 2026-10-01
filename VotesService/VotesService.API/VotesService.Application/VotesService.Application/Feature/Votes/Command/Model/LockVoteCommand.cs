using MediatR;
using System.Text.Json.Serialization;
using VotesService.Application.Bases;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Command.Model
{
    public record LockVoteCommand : IRequest<Response<bool>>
    {
        public string TargetId { get; init; } = null!;
        public VoteTargetType TargetType { get; init; }
        public bool IsLocked { get; init; } = true;   // true = Close ، false = Open

        [JsonIgnore]
        public Guid UserId { get; set; }
    }

}

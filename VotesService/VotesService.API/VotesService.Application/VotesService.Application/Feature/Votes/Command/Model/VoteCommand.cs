using MediatR;
using System.Text.Json.Serialization;
using VotesService.Application.Bases;
using VotesService.Domain.Enum;

namespace VotesService.Application.Feature.Votes.Command.Model
{
    public record VoteCommand : IRequest<Response<string>>
    {
        public string TargetId { get; init; } = null!;
        public VoteTargetType TargetType { get; init; }
        public VoteType Type { get; init; }

        [JsonIgnore]
        public Guid UserId { get; set; }   // From JWT Token


    }
}

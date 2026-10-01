using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VotesService.API.Bases;
using VotesService.Application.Bases;
using VotesService.Application.Feature.Votes.Command.Model;
using VotesService.Application.Feature.Votes.Query.Model;
using VotesService.Application.Feature.Votes.Query.Results;
using VotesService.Domain.Enum;

namespace VotesService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : AppBaseController
    {
        [HttpPost]

        [SwaggerOperation(
            Summary = "Votes on a post or comment",
            Description = "Adds a new vote, changes an existing vote, or removes it (toggle) when the same vote type is sent again.")]
        [SwaggerResponse(200, "Vote added, changed, or removed successfully", type: typeof(Response<string>))]
        [SwaggerResponse(400, "Invalid vote data or voting is locked", type: typeof(Response<string>))]
        [SwaggerResponse(500, "An unexpected error occurred")]

        public async Task<IActionResult> Vote([FromBody] VoteCommand command, CancellationToken cancellationToken)
        {
            command.UserId = new Guid("11111111-1111-1111-1111-111111111111"); //    Auth
            return NewResult(await Mediator.Send(command, cancellationToken));
        }
        [HttpGet("summary")]
        [SwaggerOperation(
            Summary = "Gets the vote summary of a post or comment",
            Description = "Returns the number of up votes, down votes, and the final score for the given target.")]
        [SwaggerResponse(200, "Vote summary retrieved successfully", type: typeof(Response<GetVoteSummaryResult>))]
        [SwaggerResponse(400, "Invalid target id", type: typeof(Response<GetVoteSummaryResult>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> GetSummary(
        [FromQuery] VoteTargetType targetType,
    [FromQuery] string targetId,
    CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetVoteSummaryQuery(targetType, targetId), cancellationToken));
        }

        [HttpGet("mine")]
        [SwaggerOperation(
            Summary = "Gets the current user's vote",
            Description = "Returns the vote type of the current user on the given target. The type is null if the user has not voted.")]
        [SwaggerResponse(200, "User vote retrieved successfully", type: typeof(Response<GetUserVoteResult>))]
        [SwaggerResponse(400, "Invalid target id or user", type: typeof(Response<GetUserVoteResult>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> GetMyVote(
            [FromQuery] VoteTargetType targetType,
            [FromQuery] string targetId,
            CancellationToken cancellationToken)
        {
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            return NewResult(await Mediator.Send(new GetUserVoteQuery(targetType, targetId, userId), cancellationToken));
        }

        [HttpPatch("lock")]
        [SwaggerOperation(
            Summary = "Locks or unlocks voting on a post or comment",
            Description = "Set isLocked to true to lock voting, or false to unlock it.")]
        [SwaggerResponse(200, "Lock state updated successfully", type: typeof(Response<bool>))]
        [SwaggerResponse(400, "Invalid target id", type: typeof(Response<bool>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> Lock([FromBody] LockVoteCommand command, CancellationToken cancellationToken)
        {
            command.UserId = new Guid("11111111-1111-1111-1111-111111111111"); // Auth
            return NewResult(await Mediator.Send(command, cancellationToken));
        }
    }
}

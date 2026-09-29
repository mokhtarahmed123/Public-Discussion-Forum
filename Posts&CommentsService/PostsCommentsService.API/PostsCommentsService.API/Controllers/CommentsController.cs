using Microsoft.AspNetCore.Mvc;
using PostsCommentsService.API.Bases;
using PostsCommentsService.Application.Feature.Comments.Command.Model;
using PostsCommentsService.Application.Feature.Comments.Query.Model;

namespace PostsCommentsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : AppBaseController
    {
        [HttpGet]
        public IActionResult Ping() => Ok(" Posts API is running.");

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            command.UserId = new Guid("11111111-1111-1111-1111-111111111111");
            return NewResult(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand command, CancellationToken cancellationToken)
        {
            command.UserId = new Guid("11111111-1111-1111-1111-111111111111");
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            return NewResult(await Mediator.Send(new DeleteCommentCommand(id, userId), cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetCommentByIdQuery(id), cancellationToken));
        }
        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetByPost(string postId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return NewResult(await Mediator.Send(new GetCommentsByPostQuery(postId, page, pageSize), cancellationToken));
        }
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUser(Guid userId, CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetCommentsByUserQuery(userId), cancellationToken));
        }

        [HttpGet("{id}/replies")]
        public async Task<IActionResult> GetReplies(string id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return NewResult(await Mediator.Send(new GetRepliesQuery(id, page, pageSize), cancellationToken));
        }
    }
}

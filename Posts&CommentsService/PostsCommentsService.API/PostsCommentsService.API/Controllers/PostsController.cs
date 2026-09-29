using Microsoft.AspNetCore.Mvc;
using PostsCommentsService.API.Bases;
using PostsCommentsService.Application.Feature.Comments.Command.Model;
using PostsCommentsService.Application.Feature.Comments.Query.Model;
using PostsCommentsService.Application.Feature.Posts.Query.Model;

namespace PostsCommentsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : AppBaseController
    {
        [HttpGet("Start")]
        public IActionResult Ping() => Ok(" Posts API is running.");

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostCommand command, CancellationToken cancellationToken)
        {

            command.UserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var response = await Mediator.Send(command, cancellationToken);
            return NewResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse("11111111-1111-1111-1111-111111111111"); // مؤقت لحد الـ Auth

            var response = await Mediator.Send(new DeletePostCommand(id, userId), cancellationToken);
            return NewResult(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePostCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            command.UserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var response = await Mediator.Send(command, cancellationToken);
            return NewResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetAllPostsQuery(), cancellationToken));
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return NewResult(await Mediator.Send(new GetLatestPostsQuery(page, pageSize), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetPostByIdQuery(id), cancellationToken));
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUser(Guid userId, CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetPostsByUserQuery(userId), cancellationToken));
        }
        [HttpGet("{id}/replies")]
        public async Task<IActionResult> GetReplies(string id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return NewResult(await Mediator.Send(new GetRepliesQuery(id, page, pageSize), cancellationToken));
        }
    }
}

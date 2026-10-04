using Microsoft.AspNetCore.Mvc;
using RankingService.API.Bases;
using RankingService.Application.Bases;
using RankingService.Application.Feature.Comments.Command.Model;
using RankingService.Application.Feature.Comments.Query.Model;
using RankingService.Application.Feature.Posts.Command.Model;
using RankingService.Application.Feature.Posts.Query.Model;
using RankingService.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RankingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : AppBaseController
    {

        [HttpPost("top-ten-posts")]
        [SwaggerOperation(
       Summary = "Refreshes the top ten posts",
       Description = "Fetches the ten most voted posts from VotesService, loads their details from PostsCommentsService, and replaces the stored top ten list.")]
        [SwaggerResponse(200, "Top ten posts updated successfully", type: typeof(Response<string>))]
        [SwaggerResponse(400, "No voted posts found", type: typeof(Response<string>))]
        [SwaggerResponse(404, "Posts not found", type: typeof(Response<string>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> AddTopTenPosts(CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new AddTopTenPostsCommand(), cancellationToken));
        }

        [HttpPost("posts/{postId}/top-comments")]
        [SwaggerOperation(
     Summary = "Refreshes the top comments of a post",
     Description = "Fetches the comments of the given post from PostsCommentsService, picks the top ones, and replaces the stored top comments for that post.")]
        [SwaggerResponse(200, "Top comments updated successfully", type: typeof(Response<string>))]
        [SwaggerResponse(400, "Invalid post id or paging values", type: typeof(Response<string>))]
        [SwaggerResponse(404, "No comments found for this post", type: typeof(Response<string>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> AddTopTenCommentsByPostId(
     [FromRoute] string postId,
     [FromQuery] int pageNumber = 1,
     [FromQuery] int pageSize = 10,
     CancellationToken cancellationToken = default)
        {
            return NewResult(await Mediator.Send(
                new AddTopTenCommentByPostIdCommand(postId, pageNumber, pageSize),
                cancellationToken));
        }

        [HttpGet("top-ten-posts")]
        [SwaggerOperation(
    Summary = "Gets the top ten posts",
    Description = "Returns the stored top ten posts ordered by score (highest first).")]
        [SwaggerResponse(200, "Top ten posts returned successfully", type: typeof(Response<List<TopTenPosts>>))]
        [SwaggerResponse(404, "No top ten posts stored yet", type: typeof(Response<List<TopTenPosts>>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> GetTopTenPosts(CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetTopTenPostsQuery(), cancellationToken));
        }

        [HttpGet("posts/{postId}/top-comments")]
        [SwaggerOperation(
    Summary = "Gets the top comments of a post",
    Description = "Returns the stored top comments for the given post ordered by score (highest first).")]
        [SwaggerResponse(200, "Top comments returned successfully", type: typeof(Response<List<TopTenComments>>))]
        [SwaggerResponse(400, "Invalid post id", type: typeof(Response<List<TopTenComments>>))]
        [SwaggerResponse(404, "No stored comments for this post", type: typeof(Response<List<TopTenComments>>))]
        [SwaggerResponse(500, "An unexpected error occurred")]
        public async Task<IActionResult> GetTopTenCommentsByPostId(
    [FromRoute] string postId, CancellationToken cancellationToken)
        {
            return NewResult(await Mediator.Send(new GetTopTenCommentByPostIdQuery(postId), cancellationToken));
        }
    }
}

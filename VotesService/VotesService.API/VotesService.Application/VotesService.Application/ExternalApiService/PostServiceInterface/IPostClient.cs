using VotesService.Application.dtos;

namespace VotesService.Application.ExternalApiService.PostServiceInterface
{
    public interface IPostClient
    {
        Task<GetPostByIdResult> GetPostByIdAsync(string postId, CancellationToken cancellationToken = default);
    }
}

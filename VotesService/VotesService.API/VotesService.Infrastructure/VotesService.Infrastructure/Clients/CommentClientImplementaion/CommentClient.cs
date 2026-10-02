using System.Net;
using System.Net.Http.Json;
using VotesService.Application.dtos;
using VotesService.Application.ExternalApiService.CommentServiceInterface;

namespace VotesService.Infrastructure.Clients.CommentClientImplementaion
{
    public class CommentClient : ICommentClient
    {
        private readonly HttpClient httpClient;

        public CommentClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GetCommentByIdResult?> GetCommentByIdAsync(string postId, string commentId, CancellationToken cancellationToken = default)
        {
            using var response = await httpClient.GetAsync(
                $"api/posts/{postId}/comments/{commentId}", cancellationToken);


            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var wrapper = await response.Content
                .ReadFromJsonAsync<ApiResponse<GetCommentByIdResult>>(cancellationToken: cancellationToken);

            return wrapper?.Data;
        }
    }
}
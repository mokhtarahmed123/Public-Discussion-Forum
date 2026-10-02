using System.Net;
using System.Net.Http.Json;
using VotesService.Application.dtos;
using VotesService.Application.ExternalApiService.PostServiceInterface;

namespace VotesService.Infrastructure.Clients.PostClientImplementaion
{
    public class PostClient : IPostClient
    {
        private readonly HttpClient httpClient;

        public PostClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<GetPostByIdResult> GetPostByIdAsync(string postId, CancellationToken cancellationToken = default)
        {
            using var response = await httpClient.GetAsync(
                $"api/Posts/{postId}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<GetPostByIdResult>>(cancellationToken: cancellationToken);

            return wrapper?.Data;
        }
    }
}

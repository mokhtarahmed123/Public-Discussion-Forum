using RankingService.Application.ExternalApiService.PostServiceInterface;
using RankingService.Application.Models;
using RankingService.Infrastructure.dtos;
using System.Net;
using System.Net.Http.Json;

namespace RankingService.Infrastructure.Client.PostClientImplementaion
{
    public class PostClient : IPostClient
    {
        private readonly HttpClient httpClient;

        public PostClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PostSnapshot> GetpostById(string postId, CancellationToken cancellationToken)
        {

            using var response = await httpClient.GetAsync(
             $"api/Posts/{postId}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<PostSnapshot>>(cancellationToken: cancellationToken);

            return wrapper?.Data;
        }
    }
}

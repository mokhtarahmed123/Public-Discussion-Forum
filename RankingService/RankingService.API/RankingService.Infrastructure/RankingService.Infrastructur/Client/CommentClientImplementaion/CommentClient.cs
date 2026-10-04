using RankingService.Application.Bases;
using RankingService.Application.ExternalApiService.CommentServiceInterface;
using RankingService.Application.Models;
using System.Net.Http.Json;

namespace RankingService.Infrastructure.Client.CommentClientImplementaion
{
    public class CommentClient : ICommentClient
    {
        private readonly HttpClient httpClient;

        public CommentClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<CommentSnapshot>> GetCommentsByPostIdAsync(string postId, int page, int pageSize, CancellationToken ct)
        {
            var response = await httpClient.GetAsync(
               $"api/post/{postId}/comments?page={page}&pageSize={pageSize}", ct);

            if (!response.IsSuccessStatusCode)
                return new();

            var result = await response.Content
                .ReadFromJsonAsync<Response<List<CommentSnapshot>>>(cancellationToken: ct);

            return result?.Data ?? new();
        }
    }
}


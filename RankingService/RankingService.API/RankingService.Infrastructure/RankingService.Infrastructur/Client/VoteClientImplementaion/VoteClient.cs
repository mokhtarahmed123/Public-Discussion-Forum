using RankingService.Application.ExternalApiService.VoteServiceInterface;
using RankingService.Application.Models;
using RankingService.Infrastructure.dtos;
using System.Net;
using System.Net.Http.Json;

namespace RankingService.Infrastructure.Client.VoteClientImplementaion
{
    public class VoteClient : IVoteClient
    {
        private readonly HttpClient httpClient;

        public VoteClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<TopVoteResult>> GetTopTenVotedCommentsAsync(CancellationToken cancellationToken)
        {
            using var response = await httpClient.GetAsync($"api/Votes/TopTenComments", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<List<TopVoteResult>>>(cancellationToken: cancellationToken);
            return wrapper?.Data;

        }

        public async Task<List<TopVoteResult>> GetTopTenVotedPostsAsync(CancellationToken cancellationToken)
        {
            using var response = await httpClient.GetAsync($"api/Votes/TopTenPosts", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<List<TopVoteResult>>>(cancellationToken: cancellationToken);
            return wrapper?.Data;
        }
    }
}

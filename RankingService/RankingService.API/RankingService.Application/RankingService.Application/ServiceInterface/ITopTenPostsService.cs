using RankingService.Domain.Entities;

namespace RankingService.Application.ServiceInterface
{
    public interface ITopTenPostsService
    {
        Task<TopTenPosts> Add(TopTenPosts topTenPosts, CancellationToken cancellationToken);
        Task<List<TopTenPosts>> GetAll(CancellationToken cancellationToken);
        Task<TopTenPosts> GetById(string id, CancellationToken cancellationToken);
        Task AddRange(ICollection<TopTenPosts> topTenPosts, CancellationToken cancellationToken);
        Task Delete(string Id, CancellationToken cancellationToken);
        Task ReplaceAll(ICollection<TopTenPosts> topTenPosts, CancellationToken cancellationToken);

    }
}

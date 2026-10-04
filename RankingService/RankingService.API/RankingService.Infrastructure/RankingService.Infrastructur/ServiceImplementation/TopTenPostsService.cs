using RankingService.Application.RepositoryInterface;
using RankingService.Application.ServiceInterface;
using RankingService.Domain.Entities;

namespace RankingService.Infrastructure.ServiceImplementation
{
    public record TopTenPostsService : ITopTenPostsService
    {
        private readonly ITopTenPostsRepository topTenPostsRepository;

        public TopTenPostsService(ITopTenPostsRepository topTenPostsRepository)
        {
            this.topTenPostsRepository = topTenPostsRepository;
        }
        public async Task<TopTenPosts> Add(TopTenPosts topTenPosts, CancellationToken cancellationToken)
        {

            return await topTenPostsRepository.AddAsync(topTenPosts, cancellationToken);

        }

        public async Task AddRange(ICollection<TopTenPosts> topTenPosts, CancellationToken cancellationToken)
        {
            await topTenPostsRepository.AddRangeAsync(topTenPosts, cancellationToken);

        }

        public async Task Delete(string Id, CancellationToken cancellationToken)
        {
            var topten = await topTenPostsRepository.GetByIdAsync(x => x.Id == Id, cancellationToken);
            await topTenPostsRepository.DeleteAsync(topten, cancellationToken);
        }

        public async Task<List<TopTenPosts>> GetAll(CancellationToken cancellationToken)
        {
            return await topTenPostsRepository.GetAll(cancellationToken);


        }

        public async Task<TopTenPosts> GetById(string id, CancellationToken cancellationToken)
        {
            return await topTenPostsRepository.GetByIdAsync(x => x.Id == id, cancellationToken);


        }

        public async Task ReplaceAll(ICollection<TopTenPosts> topTenPosts, CancellationToken cancellationToken)
        {
            var old = await topTenPostsRepository.GetAll(cancellationToken);

            if (old.Count > 0)
                await topTenPostsRepository.DeleteRangeAsync(old, cancellationToken);

            await topTenPostsRepository.AddRangeAsync(topTenPosts, cancellationToken);
        }
    }
}

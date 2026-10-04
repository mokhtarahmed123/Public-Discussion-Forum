using RankingService.Application.RepositoryInterface;
using RankingService.Domain.Entities;
using RankingService.Infrastructure.DataBaseConfiguration;
using RankingService.Infrastructure.InfrastructureBases;

namespace RankingService.Infrastructure.RepositoryImplementation
{
    public class TopTenPostsRepository : GenericRepositoryAsync<TopTenPosts>, ITopTenPostsRepository
    {
        public TopTenPostsRepository(ForumService forumService) : base(forumService)
        {
        }
    }
}

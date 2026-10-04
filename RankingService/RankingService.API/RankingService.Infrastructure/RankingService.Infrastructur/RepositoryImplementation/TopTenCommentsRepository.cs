using RankingService.Application.RepositoryInterface;
using RankingService.Domain.Entities;
using RankingService.Infrastructure.DataBaseConfiguration;
using RankingService.Infrastructure.InfrastructureBases;

namespace RankingService.Infrastructure.RepositoryImplementation
{
    public class TopTenCommentsRepository : GenericRepositoryAsync<TopTenComments>, ITopTenCommentsRepository
    {
        public TopTenCommentsRepository(ForumService forumService) : base(forumService)
        {
        }
    }
}

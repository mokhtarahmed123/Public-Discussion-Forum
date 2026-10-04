using RankingService.Application.Interface;
using RankingService.Domain.Entities;

namespace RankingService.Application.RepositoryInterface
{
    public interface ITopTenCommentsRepository : IGenericRepositoryAsync<TopTenComments>
    {
    }
}

using VotesService.Application.RepositoryInterface;
using VotesService.Domain.Entities;
using VotesService.Infrastructure.DataBaseConfiguration;
using VotesService.Infrastructure.InfrastructureBases;

namespace VotesService.Infrastructure.RepositoryImplementation
{
    public class VotesRepository : GenericRepositoryAsync<Votes>, IVotesRepository
    {
        public VotesRepository(ForumService forumService) : base(forumService)
        {
        }
    }
}

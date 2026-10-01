using VotesService.Application.Interface;
using VotesService.Domain.Entities;

namespace VotesService.Application.RepositoryInterface
{
    public interface IVotesRepository : IGenericRepositoryAsync<Votes>
    {
    }
}

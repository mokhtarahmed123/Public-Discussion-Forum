using PostsCommentsService.Data.Interfaces;
using PostsCommentsService.Domain.Entities;

namespace PostsCommentsService.Application.RepositoryInterface
{
    public interface ICommentsRepository : IGenericRepositoryAsync<Comment>
    {
    }
}

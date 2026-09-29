using PostsCommentsService.Data.Interfaces;
using PostsCommentsService.Domain.Entities;

namespace PostsCommentsService.Application.RepositoryInterface
{
    public interface IPostsRepository : IGenericRepositoryAsync<Post>
    {
    }
}

using PostsCommentsService.Application.RepositoryInterface;
using PostsCommentsService.Domain.Entities;
using PostsCommentsService.Infrastructure.DataBaseConfiguration;
using PostsCommentsService.Infrastructure.InfrastructureBases;

namespace PostsCommentsService.Infrastructure.RepositoryImplementaion
{
    public class PostsRepository : GenericRepositoryAsync<Post>, IPostsRepository
    {
        public PostsRepository(ForumService forumService) : base(forumService)
        {
        }
    }
}

using PostsCommentsService.Application.RepositoryInterface;
using PostsCommentsService.Domain.Entities;
using PostsCommentsService.Infrastructure.DataBaseConfiguration;
using PostsCommentsService.Infrastructure.InfrastructureBases;

namespace PostsCommentsService.Infrastructure.RepositoryImplementaion
{
    public class CommentsRepository : GenericRepositoryAsync<Comment>, ICommentsRepository
    {
        public CommentsRepository(ForumService forumService) : base(forumService)
        {
        }
    }
}

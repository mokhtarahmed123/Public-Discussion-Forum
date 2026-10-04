using RankingService.Application.RepositoryInterface;
using RankingService.Application.ServiceInterface;
using RankingService.Domain.Entities;

namespace RankingService.Infrastructure.ServiceImplementation
{
    public class TopTenCommentsService : ITopTenCommentsService
    {
        private readonly ITopTenCommentsRepository topTenCommentsRepository;

        public TopTenCommentsService(ITopTenCommentsRepository topTenCommentsRepository)
        {
            this.topTenCommentsRepository = topTenCommentsRepository;
        }

        public async Task<TopTenComments> Add(TopTenComments comment, CancellationToken cancellationToken)
        {
            return await topTenCommentsRepository.AddAsync(comment, cancellationToken);
        }

        public async Task AddRange(ICollection<TopTenComments> comments, CancellationToken cancellationToken)
        {
            if (comments is null || comments.Count == 0)
                return;

            await topTenCommentsRepository.AddRangeAsync(comments, cancellationToken);
        }

        public async Task Delete(string id, CancellationToken cancellationToken)
        {
            var comment = await topTenCommentsRepository.GetByIdAsync(x => x.Id == id, cancellationToken);
            if (comment is null)
                return;

            await topTenCommentsRepository.DeleteAsync(comment, cancellationToken);
        }

        public async Task DeleteByPostId(string postId, CancellationToken cancellationToken)
        {
            var all = await topTenCommentsRepository.GetAll(cancellationToken);
            var forPost = all.Where(c => c.PostId == postId).ToList();

            if (forPost.Count > 0)
                await topTenCommentsRepository.DeleteRangeAsync(forPost, cancellationToken);
        }

        public async Task<List<TopTenComments>> GetAll(CancellationToken cancellationToken)
        {
            return await topTenCommentsRepository.GetAll(cancellationToken);
        }

        public async Task<TopTenComments?> GetById(string id, CancellationToken cancellationToken)
        {
            return await topTenCommentsRepository.GetByIdAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<TopTenComments>> GetByPostId(string postId, CancellationToken cancellationToken)
        {
            var all = await topTenCommentsRepository.GetAll(cancellationToken);

            return all
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.Score)
                .ToList();
        }

        public async Task ReplaceAll(ICollection<TopTenComments> comments, CancellationToken cancellationToken)
        {
            var old = await topTenCommentsRepository.GetAll(cancellationToken);

            if (old.Count > 0)
                await topTenCommentsRepository.DeleteRangeAsync(old, cancellationToken);

            if (comments is not null && comments.Count > 0)
                await topTenCommentsRepository.AddRangeAsync(comments, cancellationToken);
        }

        public async Task ReplaceForPost(string postId, ICollection<TopTenComments> comments, CancellationToken cancellationToken)
        {
            var all = await topTenCommentsRepository.GetAll(cancellationToken);
            var oldForPost = all.Where(c => c.PostId == postId).ToList();

            if (oldForPost.Count > 0)
                await topTenCommentsRepository.DeleteRangeAsync(oldForPost, cancellationToken);

            if (comments is not null && comments.Count > 0)
                await topTenCommentsRepository.AddRangeAsync(comments, cancellationToken);
        }
    }
}
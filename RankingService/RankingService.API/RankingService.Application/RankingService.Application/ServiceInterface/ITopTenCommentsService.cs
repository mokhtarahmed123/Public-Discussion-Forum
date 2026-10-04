using RankingService.Domain.Entities;

namespace RankingService.Application.ServiceInterface
{
    public interface ITopTenCommentsService
    {
        Task<TopTenComments> Add(TopTenComments comment, CancellationToken cancellationToken);
        Task AddRange(ICollection<TopTenComments> comments, CancellationToken cancellationToken);
        Task<TopTenComments?> GetById(string id, CancellationToken cancellationToken);
        Task<List<TopTenComments>> GetAll(CancellationToken cancellationToken);
        Task Delete(string id, CancellationToken cancellationToken);

        // مناسبة لحالتك
        Task ReplaceAll(ICollection<TopTenComments> comments, CancellationToken cancellationToken);
        Task<List<TopTenComments>> GetByPostId(string postId, CancellationToken cancellationToken);
        Task DeleteByPostId(string postId, CancellationToken cancellationToken);
        Task ReplaceForPost(string postId, ICollection<TopTenComments> comments, CancellationToken cancellationToken);


    }
}

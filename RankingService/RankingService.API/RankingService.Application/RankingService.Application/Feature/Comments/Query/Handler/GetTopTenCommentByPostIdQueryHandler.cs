using MediatR;
using RankingService.Application.Bases;
using RankingService.Application.Feature.Comments.Query.Model;
using RankingService.Application.ServiceInterface;
using RankingService.Domain.Entities;

namespace RankingService.Application.Feature.Comments.Query.Handler
{
    public class GetTopTenCommentByPostIdQueryHandler : ResponseHandler,
        IRequestHandler<GetTopTenCommentByPostIdQuery, Response<List<TopTenComments>>>
    {
        private readonly ITopTenCommentsService topTenCommentsService;

        public GetTopTenCommentByPostIdQueryHandler(ITopTenCommentsService topTenCommentsService)
        {
            this.topTenCommentsService = topTenCommentsService;
        }

        public async Task<Response<List<TopTenComments>>> Handle(
            GetTopTenCommentByPostIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await topTenCommentsService.GetByPostId(request.PostId, cancellationToken);

            if (comments is null || comments.Count == 0)
                return NotFound<List<TopTenComments>>("مفيش كومنتات متخزنة للبوست ده.");

            var ordered = comments
                .OrderByDescending(c => c.Score)
                .Take(10)
                .ToList();

            return Success(ordered);
        }
    }
}
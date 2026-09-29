using MediatR;
using PostsCommentsService.Application.Bases;
using PostsCommentsService.Application.Feature.Comments.Command.Model;
using PostsCommentsService.Application.ServiceInterface;

namespace PostsCommentsService.Application.Feature.Comments.Command.Handler
{
    public class DeleteCommentCommandHandler : ResponseHandler, IRequestHandler<DeleteCommentCommand, Response<string>>
    {
        private readonly ICommentsService commentsService;
        private readonly IPostsService postsService;

        public DeleteCommentCommandHandler(ICommentsService commentsService, IPostsService postsService)
        {
            this.commentsService = commentsService;
            this.postsService = postsService;
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await commentsService.GetByIdAsync(request.CommentId, cancellationToken);

            if (comment is null || comment.IsDeleted)
                return NotFound<string>("الكومنت مش موجود.");

            if (comment.UserId != request.UserId)
                return Forbidden<string>("مش مسموحلك تمسح الكومنت ده.");

            // له ردود: نخفي المحتوى بس عشان الردود متبقاش من غير أب
            if (comment.RepliesCount > 0)
            {
                comment.Content = "تم حذف التعليق";
                await commentsService.UpdateAsync(comment, cancellationToken);
                return Deleted<string>();
            }

            // مفيش ردود: soft delete
            comment.IsDeleted = true;
            comment.DeletedAt = DateTime.UtcNow;
            await commentsService.UpdateAsync(comment, cancellationToken);

            // العدادات
            await postsService.IncrementCommentsCountAsync(comment.PostId, -1, cancellationToken);

            if (comment.ParentCommentId is not null)
                await commentsService.IncrementRepliesCountAsync(comment.ParentCommentId, -1, cancellationToken);

            return Deleted<string>();
        }
    }
}
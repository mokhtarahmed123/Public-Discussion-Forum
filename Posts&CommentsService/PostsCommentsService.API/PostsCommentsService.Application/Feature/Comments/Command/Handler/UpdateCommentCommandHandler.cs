using MediatR;
using PostsCommentsService.Application.Bases;
using PostsCommentsService.Application.Feature.Comments.Command.Model;
using PostsCommentsService.Application.ServiceInterface;

namespace PostsCommentsService.Application.Feature.Comments.Command.Handler
{
    public class UpdateCommentCommandHandler : ResponseHandler, IRequestHandler<UpdateCommentCommand, Response<string>>
    {
        private readonly ICommentsService commentsService;

        public UpdateCommentCommandHandler(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public async Task<Response<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await commentsService.GetByIdAsync(request.Id, cancellationToken);

            if (comment is null || comment.IsDeleted)
                return NotFound<string>("الكومنت مش موجود.");

            if (comment.UserId != request.UserId)
                return Forbidden<string>("مش مسموحلك تعدل الكومنت ده.");

            comment.Content = request.Content;
            comment.IsEdited = true;
            comment.UpdatedAt = DateTime.UtcNow;

            await commentsService.UpdateAsync(comment, cancellationToken);

            return Success(comment.Id);
        }
    }
}
using MediatR;
using PostsCommentsService.Application.Bases;
using System.Text.Json.Serialization;

namespace PostsCommentsService.Application.Feature.Comments.Command.Model
{
    public record CreateCommentCommand : IRequest<Response<string>>
    {
        public string PostId { get; init; } = null!;
        public string Content { get; init; } = null!;
        public string? ParentCommentId { get; init; }

        [JsonIgnore]
        public Guid UserId { get; set; }   // من التوكن (مؤقتاً ثابت)
    }
}

using AutoMapper;
using MediatR;
using PostsCommentsService.Application.Bases;
using PostsCommentsService.Application.Feature.Comments.Command.Model;
using PostsCommentsService.Application.ServiceInterface;
using PostsCommentsService.Domain.Entities;

namespace PostsCommentsService.Application.Feature.Posts.Command
{
    public class CreatePostCommandHandler : ResponseHandler, IRequestHandler<CreatePostCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly IPostsService postsService;

        public CreatePostCommandHandler(IMapper mapper, IPostsService postsService)
        {
            this.mapper = mapper;
            this.postsService = postsService;
        }
        public async Task<Response<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = mapper.Map<Post>(request);

            var created = await postsService.AddAsync(post, cancellationToken);

            return Success(created.Id);

        }
    }
}

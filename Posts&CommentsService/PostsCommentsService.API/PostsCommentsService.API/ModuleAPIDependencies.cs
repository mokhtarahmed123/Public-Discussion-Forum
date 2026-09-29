using PostsCommentsService.API.Bases;

namespace PostsCommentsService.API
{
    public static class ModuleAPIDependencies
    {
        public static IServiceCollection AddModuleApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PostsAndCommentsDatabaseSettings>(
                 configuration.GetSection("PostsAndCommentsDatabase"));
            return services;
        }
    }
}

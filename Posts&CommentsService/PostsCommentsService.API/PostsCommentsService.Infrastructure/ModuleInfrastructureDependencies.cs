using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostsCommentsService.Application.RepositoryInterface;
using PostsCommentsService.Application.ServiceInterface;
using PostsCommentsService.Data.Interfaces;
using PostsCommentsService.Infrastructure.DataBaseConfiguration;
using PostsCommentsService.Infrastructure.InfrastructureBases;
using PostsCommentsService.Infrastructure.RepositoryImplementaion;
using PostsCommentsService.Infrastructure.ServiceImplementaion;

namespace PostsCommentsService.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddSingleton<ForumService>();

            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<ICommentsService, CommentsService>();
            return services;
        }

    }
}

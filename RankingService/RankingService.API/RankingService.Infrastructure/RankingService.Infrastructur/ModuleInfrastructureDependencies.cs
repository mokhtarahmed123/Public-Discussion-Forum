using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankingService.Application.EventInterface;
using RankingService.Application.ExternalApiService.CommentServiceInterface;
using RankingService.Application.ExternalApiService.PostServiceInterface;
using RankingService.Application.ExternalApiService.VoteServiceInterface;
using RankingService.Application.RepositoryInterface;
using RankingService.Application.ServiceInterface;
using RankingService.Infrastructure.Client.CommentClientImplementaion;
using RankingService.Infrastructure.Client.PostClientImplementaion;
using RankingService.Infrastructure.Client.VoteClientImplementaion;
using RankingService.Infrastructure.DataBaseConfiguration;
using RankingService.Infrastructure.RepositoryImplementation;
using RankingService.Infrastructure.ServiceImplementation;

namespace RankingService.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            static Uri GetUri(IConfiguration config, string key) =>
               new(config[key] ?? throw new InvalidOperationException($"Missing configuration: {key}"));

            services.AddHttpClient<ICommentClient, CommentClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Services:PostAndComment:BaseUrl"]!);
            });
            services.AddHttpClient<IPostClient, PostClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Services:PostAndComment:BaseUrl"]!);
            });
            services.AddHttpClient<IVoteClient, VoteClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Services:Votes:BaseUrl"]!);
            });
            services.AddSingleton<ForumService>();
            services.AddScoped<IPostScoreService, PostScoreService>();
            services.AddScoped<ICommentScoreService, CommentScoreService>();
            services.AddScoped<ITopTenPostsRepository, TopTenPostsRepository>();
            services.AddScoped<ITopTenCommentsRepository, TopTenCommentsRepository>();
            services.AddScoped<ITopTenPostsService, TopTenPostsService>();
            services.AddScoped<ITopTenCommentsService, TopTenCommentsService>();
            return services;
        }






    }
}

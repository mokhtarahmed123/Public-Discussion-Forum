using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VotesService.Application.Interface;
using VotesService.Application.RepositoryInterface;
using VotesService.Application.ServiceInterface;
using VotesService.Infrastructure.DataBaseConfiguration;
using VotesService.Infrastructure.InfrastructureBases;
using VotesService.Infrastructure.RepositoryImplementation;

namespace VotesService
{
    public static class ModuleInfrastructureDependencies
    {

        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddSingleton<ForumService>();
            services.AddHostedService<VotesIndexInitializer>();
            services.AddScoped<IVotesService, VotesService.Infrastructure.ServiceImplementation.VotesService>();
            services.AddScoped<IVotesRepository, VotesRepository>();
            return services;
        }
    }
}

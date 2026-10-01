using VotesService.Application.Bases;

namespace VotesService.API
{
    public static class ModuleAPIDependencies
    {
        public static IServiceCollection AddModuleApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VotesDatabaseSettings>(
                 configuration.GetSection("VotesDatabase"));
            return services;
        }
    }
}

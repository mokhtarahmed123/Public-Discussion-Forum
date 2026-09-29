using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserService.Application.Behavior;

namespace UserService.Application
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddModuleCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ModuleCoreDependencies).Assembly));
            services.AddAutoMapper(cfg =>
    cfg.AddMaps(typeof(ModuleCoreDependencies).Assembly));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;

        }
    }
}

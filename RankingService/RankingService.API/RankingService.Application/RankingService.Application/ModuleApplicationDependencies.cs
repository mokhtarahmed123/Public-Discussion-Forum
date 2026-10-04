using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankingService.Application.Bases;
using RankingService.Application.Behavior;
using RankingService.Application.Consumers;
using RankingService.Application.Interface;
using System.Reflection;

namespace RankingService.Application
{
    public static class ModuleApplicationDependencies
    {
        public static void AddModuleApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ModuleApplicationDependencies).Assembly));

            services.AddAutoMapper(cfg =>
                cfg.AddMaps(typeof(ModuleApplicationDependencies).Assembly));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IResponseHandler, ResponseHandler>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<VotePostConsumer>();
                x.AddConsumer<VoteCommentConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:Host"], configuration["RabbitMq:VirtualHost"] ?? "/", h =>
                    {
                        h.Username(configuration["RabbitMq:Username"]!);
                        h.Password(configuration["RabbitMq:Password"]!);
                    });
                    cfg.ConfigureEndpoints(ctx);

                });
            });
        }
    }
}

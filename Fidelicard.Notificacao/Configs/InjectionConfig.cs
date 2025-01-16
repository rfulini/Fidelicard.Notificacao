using Fidelicard.Notificacao.Core.Interface;
using Fidelicard.Notificacao.Core.Service;
using Fidelicard.Notificacao.Infra.Config;
using Fidelicard.Notificacao.Infra.EntityMapping.AutoMapper;
using Fidelicard.Notificacao.Infra.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fidelicard.Notificacao.Configs
{
    public static class InjectionConfig
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, WebApplicationBuilder builder)
        {
            ConfigureHttpClient(services, builder.Configuration);

            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddTransient<INotificacaoRepository, NotificacaoRepository>();

            services.AddTransient<INotificacaoService, NotificacaoService>();

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                automapper.AddProfile(new AutoMapperProfile());
            }, typeof(AutoMapperProfile).Assembly);

            return services;
        }

        private static void ConfigureHttpClient(IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var config = configuration.GetSection("WebAPI.Services.Communication");
        }
    }
}

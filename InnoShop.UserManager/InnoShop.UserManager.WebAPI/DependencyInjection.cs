using InnoShop.Shared.Application.Interfaces;
using InnoShop.Shared.Infrastructure.Services;
using InnoShop.UserManager.Application;
using InnoShop.UserManager.Infrastructure;
using InnoShop.UserManager.WebAPI.Extension;

namespace InnoShop.UserManager.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddInfrastructure(configuration);

            services.AddApplication();

            services.AddSwaggerDocumentation();

            services.AddJwtAuthentication(configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
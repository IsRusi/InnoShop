using InnoShop.UserManager.Application;
using InnoShop.UserManager.Infrastructure;
using InnoShop.Shared.Application.Interfaces;
using InnoShop.Shared.Infrastructure.Services;

namespace InnoShop.UserManager.WebAPI.Extension
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add HTTP Context Accessor FIRST - needed by CurrentUserService
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

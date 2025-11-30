using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Infrastructure.Data;
using InnoShop.ProductManagment.Infrastructure.Options;
using InnoShop.ProductManagment.Infrastructure.Repositories;
using InnoShop.Shared.Application.Interfaces;
using InnoShop.Shared.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InnoShop.ProductManagment.Infrastructure
{
    public static class DependencyInjection
    {
        private const string ConnectionStringSectionName = "DefaultConnection";
        private const string ProductServiceOptionsSectionName = "ProductServiceOptions";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<ProductManagementDbContext>(options =>
                options.UseNpgsql(cfg.GetConnectionString(ConnectionStringSectionName)));
            services.AddOptions<ProductServiceOptions>(cfg.GetSection(ProductServiceOptionsSectionName).Value);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
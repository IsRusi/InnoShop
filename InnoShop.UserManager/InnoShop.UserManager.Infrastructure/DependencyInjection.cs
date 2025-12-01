using InnoShop.Shared.Application.Interfaces;
using InnoShop.Shared.Infrastructure.Services;
using InnoShop.UserManager.Application.Common.Settings;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Infrastructure.Clients;
using InnoShop.UserManager.Infrastructure.Data;
using InnoShop.UserManager.Infrastructure.Options;
using InnoShop.UserManager.Infrastructure.Repositories;
using InnoShop.UserManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InnoShop.UserManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration cfg)
        {
            service.AddLogging();
            service.AddSettings(cfg);
            service.AddDbContext<UserContext>(options =>
        options.UseNpgsql(cfg.GetConnectionString("DefaultConnection")));

            var productServiceSettings = cfg.GetSection("ProductServiceSettings").Get<ProductServiceSettings>()
                ?? throw new InvalidOperationException("ProductServiceSettings not configured");

            service.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
            {
                client.BaseAddress = new Uri(productServiceSettings.BaseAddress);
                client.DefaultRequestHeaders.Add(productServiceSettings.HeaderName, productServiceSettings.ServiceKey);
            });

            service.AddScoped<IEmailService, EmailSender>();
            service.AddScoped<IJwtService, JwtService>();
            service.AddScoped<ICurrentUserService, CurrentUserService>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            service.AddScoped<IEmailConfirmationTokenRepository, EmailConfirmationTokenRepository>();
            service.AddScoped<IPasswordHasher, PasswordHasher>();
            return service;
        }

        public static IServiceCollection AddSettings(this IServiceCollection service, IConfiguration cfg)
        {
            service.Configure<JwtSettings>(cfg.GetSection(JwtSettings.section));
            service.Configure<SmtpSettings>(cfg.GetSection(SmtpSettings.section));
            service.Configure<AppSettings>(cfg.GetSection(AppSettings.section));
            service.Configure<PasswordResetTokenSettings>(cfg.GetSection("PasswordResetTokenSettings"));
            service.Configure<ProductServiceSettings>(cfg.GetSection("ProductServiceSettings"));
            return service;
        }
    }
}
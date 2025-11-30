using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Infrastructure.Data;
using InnoShop.UserManager.Infrastructure.Options;
using InnoShop.UserManager.Infrastructure.Repositories;
using InnoShop.UserManager.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoShop.UserManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service,IConfiguration cfg)
        {
            service.AddLogging();
            service.AddSettings(cfg);
            service.AddDbContext<UserContext>(options =>
        options.UseNpgsql(cfg.GetConnectionString("DefaultConnection")));
            //service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IEmailService, EmailSender>();
            service.AddScoped<IJwtService, JwtService>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            service.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            service.AddScoped<IEmailConfirmationTokenRepository, EmailConfirmationTokenRepository>();
            service.AddScoped<IPasswordHasher, PasswordHasher>();
            //service.AddMediatR();
            return service;
        }

        public static IServiceCollection AddSettings(this IServiceCollection service,IConfiguration cfg)
        {
            service.Configure<JwtSettings>(cfg.GetSection(JwtSettings.section));
            service.Configure<SmtpSettings>(cfg.GetSection(SmtpSettings.section));
            return service;
        }
        
    }
}


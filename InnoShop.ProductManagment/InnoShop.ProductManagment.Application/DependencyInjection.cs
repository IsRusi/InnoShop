using FluentValidation;
using InnoShop.ProductManagment.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InnoShop.ProductManagment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
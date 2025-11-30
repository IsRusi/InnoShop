using FluentValidation;
using InnoShop.UserManager.Application.Common.Behaviors;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Interfaces.IService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InnoShop.UserManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {

            service.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            
            service.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            
            return service;
        }
    }
}

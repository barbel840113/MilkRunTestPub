using FluentValidation;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MilkRun.Infrastructure.Repositories;
using MilkRun.Infrastructure.Features.Products.Commands;
using MilkRun.Infrastructure.Pipelines;

namespace MilkRun.Infrastructure
{
    public static class DependencyInjectionRegistration
    {
        public static void AddInfrastrcutureServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());          
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IJsonRepository, JsonRepository>();
        }       
    }
}

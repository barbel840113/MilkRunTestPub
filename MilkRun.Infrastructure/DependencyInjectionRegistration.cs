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

namespace MilkRun.Infrastructure
{
    public static class DependencyInjectionRegistration
    {
        public static void AddInfrastrcutureServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            
        }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProductsEx.Application.Common.Interfaces.Authentication;
using ProductsEx.Application.Common.Services;
using ProductsEx.Infrastructure.Authentication;
using ProductsEx.Infrastructure.Services;

namespace ProductsEx.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProductsEx.Application.Common.Interfaces.Authentication;
using ProductsEx.Application.Common.Services;
using ProductsEx.Infrastructure.Authentication;
using ProductsEx.Infrastructure.Services;
using ProductsEx.Application.Persistence;
using ProductsEx.Infrastructure.Persistence;

namespace ProductsEx.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                          IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
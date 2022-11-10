﻿using Microsoft.EntityFrameworkCore;
using CLG.Core.Contracts;
using CLG.Core.Services;
using CLG.Infrastructure.Data;
using CLG.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiServiceCollectionExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ICredentialService, CredentialService>();

            return services;
        }

        public static IServiceCollection AddApiDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<CLGDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
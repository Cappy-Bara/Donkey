using Donkey.Core.Repositories;
using Donkey.Infrastructure.Database;
using Donkey.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUsersRepository, MockedUsersRepository>();
            services.AddScoped<IBlogsRepository, MockedBlogsRepository>();


            services.AddDbContext<DonkeyDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("Default"));
            });

            return services;
        }
    }
}

using Donkey.Core.Repositories;
using Donkey.Infrastructure.Repositories;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, MockedUsersRepository>();

            return services;
        }
    }
}

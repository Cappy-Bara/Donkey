using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;


namespace Donkey.Core
{
    public static class Setup
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

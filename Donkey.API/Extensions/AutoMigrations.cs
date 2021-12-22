using Donkey.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.Extensions
{
    public static class AutoMigrations
    {
        public static WebApplication EnableAutoMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DonkeyDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}

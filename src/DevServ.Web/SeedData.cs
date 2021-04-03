using DevServ.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;


namespace DevServ.Web
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new DevServDbContext(
                            serviceProvider.GetRequiredService<DbContextOptions<DevServDbContext>>()))
            {
                // Look for any TODO items.
                if (dbContext.Developers.Any())
                {
                    return;   // DB has been seeded
                }

                //PopulateTestData(dbContext);


            }
        }
    }
}
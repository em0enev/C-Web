using Eventures.Data;
using Eventures.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Eventures.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EventuresDbContext>();

                context.Database.EnsureCreated();

                Assembly.GetAssembly(typeof(EventuresDbContext))
                    .GetTypes()
                    .Where(x => typeof(ISeeder).IsAssignableFrom(x))
                    .Where(x => x.IsClass)
                    .Select(x => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(x))
                    .ToList()
                    .ForEach(seeder => seeder.Seed().GetAwaiter().GetResult());
            }
        }
    }
}

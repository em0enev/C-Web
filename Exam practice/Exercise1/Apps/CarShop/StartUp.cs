using CarShop.Data;
using CarShop.Services;

using SUS.HTTP;
using SUS.MvcFramework;

using System.Collections.Generic;

namespace CarShop
{
    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            using (var db = new ApplicationDbContext())
            {
              // db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ICarsService, CarsService>();
        }
    }
}

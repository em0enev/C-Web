using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventures.Data.Seeding
{
    public class EventuresUserRoleSeeder : ISeeder
    {
        public void Seed(EventuresDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER"
                });

                context.SaveChanges();
            }
        }
    }
}

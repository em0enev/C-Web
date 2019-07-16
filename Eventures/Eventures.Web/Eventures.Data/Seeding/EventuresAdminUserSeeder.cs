using Eventures.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventures.Data.Seeding
{
    public class EventuresAdminUserSeeder : ISeeder
    {
        private readonly UserManager<EventuresUser> userManager;

        public EventuresAdminUserSeeder(UserManager<EventuresUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            //var user = new EventuresUser
            //{
            //    UserName = "root",
            //    Email = "root@eventures.com",
            //    FirstName = "Root",
            //    LastName = "Root",
            //    UCN = "ROOT"
            //};

            //var createUserResult = await this.userManager.CreateAsync(user, "root");
            //var setUserToRoleResult = await this.userManager.AddToRoleAsync(user, "Admin");

            //;
        }
    }
}

using Panda.Data;
using Panda.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
    public class UserService : IUserService
    {
        private readonly PandaDbContext context;

        public UserService(PandaDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<PandaUser> GetAllUsers()
        {
            var users = this.context.Users.ToList();

            return users;
        }

        public PandaUser GetUserById(string id)
        {
            var user = this.context.Users.Find(id);

            return user;
        }
    }
}

using Panda.Domain;
using System.Collections.Generic;

namespace Panda.Services
{
    public interface IUserService
    {
        IEnumerable<PandaUser> GetAllUsers();

        PandaUser GetUserById(string id);
    }
}

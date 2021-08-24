using CarShop.Data;
using CarShop.Data.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarShop.Services
{
    public class UsersService : IUsersService
    {
        private const string mechanicRoleAsString = "Mechanic";
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, string email, string password, string userType)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = HashPassword(password),
                IsMechanic = userType == mechanicRoleAsString ? true : false
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = this.db.Users
                .Where(x => x.Username == username && x.Password == HashPassword(password))
                .FirstOrDefault();

            return user.Id.ToString();
        }

        public bool IsUserMechanic(string Userid)
        {
            var user = this.db.Users
                .Find(Guid.Parse(Userid));

            return user.IsMechanic ? true : false;
        }

        public bool IsUsernameAvailable(string username)
        {
            var user = this.db.Users
                .Where(x => x.Username == username)
                .FirstOrDefault();

            return user == null ? true : false;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}

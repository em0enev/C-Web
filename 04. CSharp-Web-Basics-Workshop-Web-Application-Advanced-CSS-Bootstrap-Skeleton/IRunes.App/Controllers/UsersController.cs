using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class UsersController : BaseController
    {
        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View();
        }
        public IHttpResponse LoginCnfirm(IHttpRequest request)
        {
            var context = new RunesDbContext();

            using (context)
            {
                string username = ((ISet<string>)request.FormData["username"]).FirstOrDefault();
                string password = ((ISet<string>)request.FormData["password"]).FirstOrDefault();

                User userFromDb = context.Users.FirstOrDefault(user => (user.Username == username || user.Email == username)
                                                                       && user.Password == this.HashPassword(password));

                if (userFromDb == null)
                {
                    return this.Redirect("/Users/Login");
                }

                this.SignIn(request, userFromDb);
            }

            return this.Redirect("/");
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View();
        }
        public IHttpResponse RegisterConfirm(IHttpRequest request)
        {
            var context = new RunesDbContext();

            using (context)
            {
                string username = ((ISet<string>)request.FormData["username"]).FirstOrDefault();
                string password = ((ISet<string>)request.FormData["password"]).FirstOrDefault();
                string confirmPassword = ((ISet<string>)request.FormData["confirmPassword"]).FirstOrDefault();
                string email = ((ISet<string>)request.FormData["email"]).FirstOrDefault();

                if (password != confirmPassword)
                {
                    return this.Redirect("/User/Register");
                }

                var user = new User()
                {
                    Username = username,
                    Email = email,
                    Password = HashPassword(password)
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return this.Redirect("/Users/Login");
        }
        public IHttpResponse Logout(IHttpRequest request)
        {
            this.SignOut(request);

            return this.Redirect("/");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                return Encoding.UTF8.GetString(hashBytes);
            }
        }
    }
}

using CarShop.Helpers;
using CarShop.Services;
using CarShop.ViewModels.User;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        // Login 
        [HttpGet]
        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginInputModel model)
        {
            if (!ModelStateValidator.IsValid(model))
            {
                return this.View();
            }

            var userId = this.userService.GetUserId(model.Username, model.Password);

            this.SignIn(userId);

            return this.Redirect("/");
        }

        // Register
        [HttpGet]
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (!ModelStateValidator.IsValid(model))
            {
                return this.Redirect("/Users/Register");
            }

            this.userService.Create(model.Username, model.Email, model.Password, model.UserType);

            return this.Redirect("Login");
        }

        //Logout

        [HttpGet]
        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}

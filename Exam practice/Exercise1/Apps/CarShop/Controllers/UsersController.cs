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
        private const string CarsAllPath = "/Cars/All";

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        // Login 
        [HttpGet]
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
               return this.Redirect(CarsAllPath);
            }

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
            if (this.IsUserSignedIn())
            {
                return this.Redirect(CarsAllPath);
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (!ModelStateValidator.IsValid(model))
            {
                return this.View();
            }

            this.userService.Create(model.Username, model.Email, model.Password, model.UserType);

            return this.Redirect(nameof(this.Login));
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

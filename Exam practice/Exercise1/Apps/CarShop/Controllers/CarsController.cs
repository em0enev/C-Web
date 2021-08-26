using CarShop.Services;
using CarShop.ViewModels.Car;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;
        private const string notLoggedInErrMsg = "You are not logged in";
        private const string notCLientErrMsg = "You are not a client";
        private const string pathCarsAll = "/Cars/All";

        public CarsController(ICarsService carsService, IUsersService usersService)
        {
            this.carsService = carsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!usersService.IsUserMechanic(this.GetUserId()))
            {
                var currentClientCars = this.carsService.GetAllCarsForCurrentClient(this.GetUserId());
                return this.View(currentClientCars);
            }

            var carsWithIssues = this.carsService.GetAllCarsWithUnfixedIssues();

            return this.View(carsWithIssues);
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error(notLoggedInErrMsg);
            }

            if (usersService.IsUserMechanic(this.GetUserId()))
            {
                return this.Error(notLoggedInErrMsg);
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCarInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error(notLoggedInErrMsg);
            }

            var userId = this.GetUserId();
            if (usersService.IsUserMechanic(userId))
            {
                return this.Error(notCLientErrMsg);
            }

            this.carsService.Add(input, userId); ;

            return this.Redirect(pathCarsAll);
        }
    }
}

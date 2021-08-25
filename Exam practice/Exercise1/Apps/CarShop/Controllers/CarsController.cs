using CarShop.Services;
using CarShop.ViewModels.Car;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

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
                return this.Error("You are not logged in");
            }

            if (usersService.IsUserMechanic(this.GetUserId()))
            {
                return this.Error("You are not client");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCarInputModel input) 
        {

            
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in");
            }

            var userId = this.GetUserId();
            if (usersService.IsUserMechanic(userId))
            {
                return this.Error("You are not client");
            }

            this.carsService.Add(input, userId); ;

            return this.Redirect("/Cars/All");
        }
    }
}

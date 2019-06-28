using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.App.Models.Packages;
using Panda.Domain;
using Panda.Services;
using System.Globalization;

namespace Panda.App.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService packageService;
        private readonly IUserService userService;

        public PackageController(IPackageService packageService, IUserService userService)
        {
            this.packageService = packageService;
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.userService.GetAllUsers();

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(PackageCreateBindingModel model)
        {
            var recipientName = this.User.Identity.Name;

            this.packageService.CreatePackage(recipientName, model.Weight, model.ShippingAddress, model.Description);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            var package = this.packageService.GetPackage(id);
                
            string deliveryDate = null;
            if (package.EstimatedDeliveryDate == null || package.Status == PackageStatus.Delivered)
            {
                deliveryDate = "N/A";
            }
            else if (package.Status == PackageStatus.Acquired)
            {
                deliveryDate = "Delivered";
            }
            else
            {
                deliveryDate = package.EstimatedDeliveryDate.Value.Date.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture);
            }

            var packageViewModel = new PackageDetailsViewModel()
            {
                Description = package.Description,
                Address = package.ShippingAddress,
                EstimatedDeliveryDate = deliveryDate,
                Recipient = package.Recipient.UserName,
                Status = package.Status.ToString(),
                Weight = package.Weight
            };

            return this.View(packageViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var packages = this.packageService.GetPackagesByStatus(PackageStatus.Pending);

            return this.View(packages);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Shipped()
        {
            var packages = this.packageService.GetPackagesByStatus(PackageStatus.Shipped);

            return this.View(packages);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Delivered()
        {
            var packages = this.packageService.GetPackagesByStatus(PackageStatus.Acquired);

            return this.View(packages);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Ship(string id)
        {
            this.packageService.ShipPackage(id);

            return this.Redirect("/Home/Index");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(string id)
        {
            this.packageService.DeliverPackage(id);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Acquire(string id)
        {
            this.packageService.AcquirePackage(id);

            return Redirect("/Home/Index");
        }
    }
}

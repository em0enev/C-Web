using Microsoft.AspNetCore.Mvc;
using Panda.Services;

namespace Panda.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPackageService packageService;

        public HomeController(IPackageService packageService)
        {
            this.packageService = packageService;
        }

        public IActionResult Index()
        {
            var packages = this.packageService.GetAllPackages();

            return this.View(packages);
        }
    }
}


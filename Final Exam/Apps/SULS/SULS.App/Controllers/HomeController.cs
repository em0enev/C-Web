using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problem;
using SULS.Services;
using System.Collections.Generic;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemService problemService;

        public HomeController(IProblemService service)
        {
            this.problemService = service;
        }


        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            var viewProblems = new List<ProblemHomeViewModel>();

            if (this.IsLoggedIn())
            {
                var problems = this.problemService.GetAllProblems();

                foreach (var problem in problems)
                {
                    var item = new ProblemHomeViewModel()
                    {
                        Id = problem.Id,
                        Name = problem.Name,
                        Count = problem.Submissions.Count
                    };

                    viewProblems.Add(item);
                }
            }

            return this.View(viewProblems);
        }
    }
}
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Services;
using System.Collections.Generic;
using System.Globalization;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;
        public ProblemsController(IProblemService service, ISubmissionService servicee)
        {
            this.submissionService = servicee;
            this.problemService = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProblemInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.problemService.CreateProblem(model.Name, model.Points);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            var submissionsViewModels = new List<SubmissionsDetailsViewModel>();

            var submissions = this.submissionService.GetAllSubmissionByProblemID(id);
            var problem = this.problemService.GetProblemById(id);

            foreach (var subm in submissions)
            {
                var submissionsDto = new SubmissionsDetailsViewModel()
                {
                    Username = subm.User.Username,
                    AchievedResult = subm.AchievedResult,
                    MaxPoints = subm.Problem.Points,
                    CreatedOn = subm.CreatedOn.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture),
                    SubmissionId = subm.Id
                };

                submissionsViewModels.Add(submissionsDto);
            }

            var dto = new SubmissionDetailsDTO()
            {
                Name = problem.Name,
                Submissions = submissionsViewModels
            };

            return this.View(dto);
        }
    }
}

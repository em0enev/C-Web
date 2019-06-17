using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Services;
using System.Collections.Generic;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService submissionService;
        private readonly IProblemService problemService;

        public SubmissionsController(ISubmissionService service, IProblemService problemService)
        {
            this.submissionService = service;
            this.problemService = problemService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(string id)
        {
            var problem = this.problemService.GetProblemById(id);

            var problemViewModel = new ProblemDetailsViewModel()
            {
                Name = problem.Name,
                ProblemId = problem.Id
            };

            return this.View(problemViewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(SubmissionInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Submissions/Create?id={model.problemId}");
            }

            submissionService.CreateSubmissionAndAddToCurrentProblem(model.problemId, this.User.Id, model.Code);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(string id)
        {
            this.submissionService.DeleteSubmissionById(id);

            return this.Redirect("/");
        }

    }
}

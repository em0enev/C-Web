using CarShop.Services;
using CarShop.ViewModels.Issue;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
{
    class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }

        [HttpGet]
        public HttpResponse Add(string carId)
        {
            return this.View(carId);
        }

        [HttpPost]
        public HttpResponse Add(AddIssueInputModel input)
        {
            this.issuesService.Add(input);
            return this.Redirect($"/Issues/CarIssues?CarId={input.CarId}");
        }

        [HttpGet]
        public HttpResponse CarIssues(string carId)
        {
            var issues = this.issuesService.GetIssuesForCurrentCar(carId);
            return this.View(issues);
        }

        [HttpGet]
        public HttpResponse Delete(string issueId, string carId)
        {
            this.issuesService.DeleteIssue(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?CarId={carId}");
        }

        [HttpGet]
        public HttpResponse Fix(string issueId, string carId)
        {
            this.issuesService.FixIssue(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?CarId={carId}");
        }
    }
}

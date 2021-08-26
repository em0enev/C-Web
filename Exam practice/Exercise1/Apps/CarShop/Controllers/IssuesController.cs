using CarShop.Services;
using CarShop.ViewModels.Issue;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;
        private readonly IUsersService usersService;
        private readonly string carIssuesPath = $"/Issues/CarIssues?CarId=";
        private const string FixIssueErrMsg = "Only mechanics can fix issues";

        public IssuesController(IIssuesService issuesService, IUsersService usersService)
        {
            this.issuesService = issuesService;
            this.usersService = usersService;
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
            return this.Redirect(carIssuesPath + input.CarId);
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

            return this.Redirect(carIssuesPath + carId);
        }

        [HttpGet]
        public HttpResponse Fix(string issueId, string carId)
        {
            if (!usersService.IsUserMechanic(this.GetUserId()))
            {
                return this.Error(FixIssueErrMsg);
            }

            this.issuesService.FixIssue(issueId, carId);

            return this.Redirect(carIssuesPath + carId);
        }
    }
}

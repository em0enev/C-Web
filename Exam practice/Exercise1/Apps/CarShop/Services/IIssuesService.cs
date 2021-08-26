using CarShop.ViewModels.Issue;

namespace CarShop.Services
{
    public interface IIssuesService
    {
        public void Add(AddIssueInputModel input);

        public void FixIssue(string issueId, string carId);

        public void DeleteIssue(string issueId, string carId);

        public IssuesSummaryViewModel GetIssuesForCurrentCar(string carId);
    }
}

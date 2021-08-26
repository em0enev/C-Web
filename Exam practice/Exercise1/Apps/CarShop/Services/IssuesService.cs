using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Issue;
using System.Linq;

namespace CarShop.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(AddIssueInputModel input)
        {
            var issue = new Issue()
            {
                Description = input.Description,
                CarId = input.CarId,
                IsFixed = false
            };

            var carFromDb = this.db.Cars.Where(x => x.Id.ToString() == input.CarId).FirstOrDefault();
            carFromDb.Issues.Add(issue);

            this.db.SaveChanges();
        }

        public void DeleteIssue(string issueId, string carId)
        {
            var issueForDelete = this.db.Cars
                .Where(c => c.Id.ToString() == carId)
                .SelectMany(i => i.Issues)
                .Where(x => x.Id.ToString() == issueId)
                .FirstOrDefault();

            this.db.Issues.Remove(issueForDelete);
            this.db.SaveChanges();
        }

        public void FixIssue(string issueId, string carId)
        {
            var issue = this.db.Cars
                .Where(c => c.Id.ToString() == carId)
                .SelectMany(i => i.Issues)
                .Where(x => x.Id.ToString() == issueId)
                .FirstOrDefault();

            issue.IsFixed = true;

            this.db.Update(issue);
            this.db.SaveChanges();
        }

        public IssuesSummaryViewModel GetIssuesForCurrentCar(string carId)
        {
            return this.db.Cars
                .Where(x => x.Id.ToString() == carId)
                .Select(x => new IssuesSummaryViewModel
                {
                    CarId = carId,
                    Model = x.Model,
                    Issues = x.Issues
                    .Select(i => new IssueViewModel
                    {
                        Description = i.Description,
                        IssueId = i.Id.ToString(),
                        IsFixed = i.IsFixed == true ? "Yes" : "Not yet"
                    })
                }).FirstOrDefault();
        }
    }
}

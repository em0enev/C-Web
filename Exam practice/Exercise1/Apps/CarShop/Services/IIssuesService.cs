using CarShop.ViewModels.Issue;
using System;
using System.Collections.Generic;
using System.Text;

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

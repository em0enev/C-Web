using System.Collections.Generic;

namespace CarShop.ViewModels.Issue
{
    public class IssuesSummaryViewModel
    {
        public string Model { get; set; }

        public string CarId { get; set; }

        public IEnumerable<IssueViewModel> Issues { get; set; }
    }
}

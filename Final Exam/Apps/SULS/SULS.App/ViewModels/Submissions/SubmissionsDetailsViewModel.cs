using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionsDetailsViewModel
    {
        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public string CreatedOn { get; set; }

        public string SubmissionId { get; set; }


        //        <td class="col-lg-3 suls-text-color">@problem.Username</td>
        //                <td class="col-lg-3 suls-text-color">@problem.AchievedResult / @problem.MaxPoints</td>
        //                <td class="col-lg-3 suls-text-color">@problem.CreatedOn</td>
        //                <td class="col-lg-3 suls-text-color">
        //}
    }
}

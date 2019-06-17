using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionDetailsDTO
    {
        public string Name { get; set; }

        public List<SubmissionsDetailsViewModel> Submissions { get; set; }
    }
}

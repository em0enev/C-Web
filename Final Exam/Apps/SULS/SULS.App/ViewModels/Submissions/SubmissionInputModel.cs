using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionInputModel
    {
        private const string errorMsg = "Your code must be between 30 and 800 symbols";

        [RequiredSis]
        [StringLengthSis(30, 800, errorMsg)]
        public string Code { get; set; }

        public string problemId { get; set; }

    }
}

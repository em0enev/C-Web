using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemInputModel
    {
        private const string pointsErrorMsg = "The points must be between 50 and 300";
        private const string nameErrorMsg= "The name must be between 5 and 20 symbols";


        [RequiredSis]
        [StringLengthSis(5,20,nameErrorMsg)]
        public string Name { get; set; }

        [RequiredSis]
        [RangeSis(50,300,pointsErrorMsg)]
        public int Points { get; set; }


//        •	Has a Name – a string with min length 5 and max length 20 (required)
//•	Has Points– an integer between 50 and 300 (required)

    }
}

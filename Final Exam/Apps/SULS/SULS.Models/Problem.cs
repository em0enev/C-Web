using System.Collections.Generic;

namespace SULS.Models
{
    public class Problem
    {
        public Problem()
        {
            this.Submissions = new List<Submission>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
          //        Problem
          //•	Has an Id – a string, Primary Key
          //•	Has a Name – a string with min length 5 and max length 20 (required)
          //•	Has Points– an integer between 50 and 300 (required)

    }
}

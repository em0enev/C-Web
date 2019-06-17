using System.Collections.Generic;

namespace SULS.Models
{
    public class User
    {
        public User()
        {
            this.Submissions = new List<Submission>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

    }
}
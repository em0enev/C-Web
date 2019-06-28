namespace Panda.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Packages = new List<Package>();
            this.Receipts = new List<Receipt>();
        }
        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}

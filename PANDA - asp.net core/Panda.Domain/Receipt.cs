using System;

namespace Panda.Domain
{
    public class Receipt
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public virtual PandaUser Recipient { get; set; }
        public string RecipientId { get; set; }

        public virtual Package Package { get; set; }
        public string PackageId { get; set; }
    }
}

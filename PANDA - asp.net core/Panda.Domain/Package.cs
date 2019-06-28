using System;

namespace Panda.Domain
{
    public class Package
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public decimal Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; } = PackageStatus.Pending;

        public DateTime? EstimatedDeliveryDate { get; set; } = null;

        public virtual PandaUser Recipient { get; set; }
        public string RecipientId { get; set; }

        public virtual Receipt Receipt { get; set; }
        public string ReceiptId { get; set; }
    }
}

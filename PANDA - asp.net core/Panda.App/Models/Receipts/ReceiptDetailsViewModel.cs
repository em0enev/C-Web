using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Panda.App.Models.Receipts
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }

        public string IssuedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal Weight { get; set; }

        public string Description { get; set; }

        public string Recipient { get; set; }

        public decimal TotalPrice { get; set; }
    }
}

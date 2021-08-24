using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Data.Models
{
    public class Issue
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsFixed { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}

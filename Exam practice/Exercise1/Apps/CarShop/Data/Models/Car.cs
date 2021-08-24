using System;
using System.Collections.Generic;

namespace CarShop.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Issues = new List<Issue>();
        }

        public Guid Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string PictureUrl { get; set; }

        public string PlateNumber { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; } // maybe private setter ? 
    }
}

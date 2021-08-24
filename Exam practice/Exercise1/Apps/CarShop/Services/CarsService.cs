using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<CarViewModel> GetAllCarsWithUnfixedIssues()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CarViewModel> GetAllCarsForCurrentClient(string userId)
        {
            return this.db.Cars
                .Where(x => x.OwnerId == userId)
                .Select(x => new CarViewModel()
                {
                    Id = x.Id.ToString(),
                    ImageUrl = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    FixedIssues = x.Issues.Where(i => i.IsFixed == true).Count(),
                    RemainingIssues = x.Issues.Where(i => i.IsFixed == false).Count(),
                })
                .ToList();
        }

        public void Add(AddCarInputModel model, string userId)
        {
            var car = new Car()
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = userId
            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }
    }
}

using CarShop.ViewModels.Car;
using System.Collections.Generic;

namespace CarShop.Services
{
    public interface ICarsService
    {
        public IEnumerable<CarViewModel> GetAllCarsWithUnfixedIssues();

        public IEnumerable<CarViewModel> GetAllCarsForCurrentClient(string userId);

        public void Add(AddCarInputModel model, string userId);
    }
}
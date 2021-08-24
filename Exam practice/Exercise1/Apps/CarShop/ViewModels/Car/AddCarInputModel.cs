namespace CarShop.ViewModels.Car
{
    public class AddCarInputModel
    {
        //TODO: Validations
        public string Model { get; set; }

        public int Year { get; set; }

        public string Image { get; set; }

        public string PlateNumber { get; set; }
    }
}

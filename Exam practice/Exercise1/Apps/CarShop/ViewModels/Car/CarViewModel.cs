namespace CarShop.ViewModels.Car
{
    public class CarViewModel
    {
        //Todo: Validation
        public string Id { get; set; }

        public string PlateNumber { get; set; }

        public string ImageUrl { get; set; }

        public int RemainingIssues { get; set; }

        public int FixedIssues { get; set; }
    }
}

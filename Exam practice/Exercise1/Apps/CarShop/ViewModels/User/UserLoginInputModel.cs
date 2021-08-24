using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels.User
{
    public class UserLoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

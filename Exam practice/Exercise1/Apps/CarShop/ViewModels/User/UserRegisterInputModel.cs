using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels.User
{
    public class UserRegisterInputModel
    {
        private const string UsernameErrorMsg = "Invalid username length! Must be between 4 and 20 symbols!";
        private const string PasswordErrorMsg = "Invalid password length! Must be between 5 and 20 symbols!";

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = UsernameErrorMsg)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = PasswordErrorMsg)]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserType { get; set; }
    }
}

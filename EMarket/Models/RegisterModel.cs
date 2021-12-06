using System.ComponentModel.DataAnnotations;

namespace EMarket.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [MinLength(8, ErrorMessage = "Must be more 8 symbols")]
        [MaxLength(16, ErrorMessage = "Must be less 16 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [MinLength(8, ErrorMessage = "Must be more 8 symbols")]
        [MaxLength(16, ErrorMessage = "Must be less 16 symbols")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
    }
}

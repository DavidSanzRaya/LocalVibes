using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Choose a username")]
        [Display(Name = "Email adress")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [Display(Name = "Profile image")]
        public byte[]? ProfileImage { get; set; }

        [Display(Name = "Document number")]
        public string? DocumentNumber { get; set; }
    }
}

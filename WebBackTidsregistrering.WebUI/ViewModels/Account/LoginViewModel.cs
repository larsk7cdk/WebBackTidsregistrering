using System.ComponentModel.DataAnnotations;

namespace WebBackTidsregistrering.WebUI.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
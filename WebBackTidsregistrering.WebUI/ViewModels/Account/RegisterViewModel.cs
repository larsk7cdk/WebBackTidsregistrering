using System.ComponentModel.DataAnnotations;

namespace WebBackTidsregistrering.WebUI.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} skal være mindst {2} og højst {1} karakterer lang.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kodeord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Skriv kodeord igen")]
        [Compare("Password", ErrorMessage = "Kodeord stemmer ikke overens.")]
        public string ConfirmPassword { get; set; }
    }
}
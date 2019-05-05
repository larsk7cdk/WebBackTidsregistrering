using System;
using System.ComponentModel.DataAnnotations;

namespace WebBackTidsregistrering.WebUI.ViewModels.Registration
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Dato")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Start tidspunkt")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Slut tidspunkt")]
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
    }
}
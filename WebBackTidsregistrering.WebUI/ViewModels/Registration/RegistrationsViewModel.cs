using System;
using System.ComponentModel.DataAnnotations;

namespace WebBackTidsregistrering.WebUI.ViewModels.Registration
{
    public class RegistrationsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Dato")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Start tidspunkt")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Slut tidspunkt")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }
    }
}

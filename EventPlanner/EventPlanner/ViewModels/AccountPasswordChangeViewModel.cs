using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class AccountPasswordChangeViewModel
    {
        [Required(ErrorMessage = "Wachtwoord is vereist")]
        [DataType(DataType.Password)]
        [Display(Name = "Huidig wachtwoord")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Tweede wachtwoord is vereist")]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord")]
        public string Password { get; set; }
    }
}

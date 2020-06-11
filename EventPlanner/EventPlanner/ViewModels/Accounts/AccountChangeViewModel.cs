using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class AccountChangeViewModel
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Email is vereist")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Voornaam is vereist")]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is vereist")]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Geboortedatum is vereist")]
        [DataType(DataType.Date)]
        [Display(Name = "Geboortedatum")]
        public DateTime DateOfBirth { get; set; }
    }
}

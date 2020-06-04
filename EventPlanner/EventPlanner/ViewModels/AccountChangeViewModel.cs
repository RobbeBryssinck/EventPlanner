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

        [Required(ErrorMessage = "Gebruikersnaam is vereist")]
        [Display(Name = "Gebruikersnaam")]
        public string Username { get; set; }

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

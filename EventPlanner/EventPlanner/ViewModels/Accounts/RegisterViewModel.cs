using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Gebruikersnaam is vereist")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Voornaam is vereist")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is vereist")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is vereist")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Geboortedatum is vereist")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Wachtwoord is vereist")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Wachtwoord en bevestiging wachtwoord komen niet overeen!")]
        public string ConfirmPassword { get; set; }
    }
}

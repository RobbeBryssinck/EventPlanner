using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Gebruikersnaam is vereist")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Wachtwoord is vereist")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Onthoud mij")]
        public bool RememberMe { get; set; }
    }
}

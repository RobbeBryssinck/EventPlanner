using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }
        
        [Display(Name = "Tussenvoegsel")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Huisnummer")]
        public int HouseNumber { get; set; }

        [Required]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        public DateTime DateOfBirth { get; set; }
    }
}

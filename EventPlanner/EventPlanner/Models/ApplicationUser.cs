using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}

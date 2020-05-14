using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class CoachAddViewModel
    {
        [Key]
        public int CoachId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Coach naam")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Vertel wat over jezelf!")]
        public string Info { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public ICollection<IFormFile> files { get; set; }

        public string ImageSrc { get; set; }
    }
}

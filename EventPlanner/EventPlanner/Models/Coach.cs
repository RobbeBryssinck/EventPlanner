using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Coach
    {
        [Key]
        public int CoachId { get; set; }

        [Display(Name = "Coach naam")]
        public string Name { get; set; }

        [Display(Name = "Informatie")]
        public string Info { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string ImageSrc { get; set; }
    }
}

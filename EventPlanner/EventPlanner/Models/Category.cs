using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Category
    {
        [Key]
        public int CategorieId { get; set; }

        [Required]
        [Display(Name ="Categorie naam")]
        public string CategorieName { get; set; }

        [Required]
        [Display(Name = "Beschrijving")]
        public string Info { get; set; }


    }
}

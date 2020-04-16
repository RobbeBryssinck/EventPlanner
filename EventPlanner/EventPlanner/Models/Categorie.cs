using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Categorie
    {
        [Key]
        public int CategorieId { get; set; }

        [Required]
        public string CategorieName { get; set; }

        [Required]
        public string Info { get; set; }


    }
}

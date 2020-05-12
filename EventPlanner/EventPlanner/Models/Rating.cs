using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Rating
    {
        [Key]
        [Required]
        public int RatingId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Display(Name = "Aantal sterren")]
        [Required]
        public int StarRating { get; set; }
        [Display(Name = "Onderwerp")]
        [Required]
        public string RatingTitle { get; set; }

        [Display(Name = "Commentaar")]
        [Required]
        public string Comment { get; set; }

    }
}

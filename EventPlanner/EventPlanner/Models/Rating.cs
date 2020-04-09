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
        public int RatingId { get; set; }
        public int EventId { get; set; }
        [Display(Name = "Aantal sterren")]
        public int StarRating { get; set; }
        [Display(Name = "Onderwerp")]
        public string RatingTitle { get; set; }

        [Display (Name = "Commentaar")]
        public string Comment { get; set; }

    }
}

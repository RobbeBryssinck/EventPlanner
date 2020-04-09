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
        public int StarRating { get; set; }
        public string RatingTitle { get; set; }
        public string Comment { get; set; }

    }
}

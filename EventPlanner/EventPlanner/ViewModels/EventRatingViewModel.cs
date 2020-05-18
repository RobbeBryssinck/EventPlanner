using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class EventRatingViewModel
    {
        public Event Event { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}

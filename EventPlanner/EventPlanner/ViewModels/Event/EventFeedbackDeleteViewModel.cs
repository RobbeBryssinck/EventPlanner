using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class EventFeedbackDeleteViewModel
    {
        public int RatingId { get; set; }
        public int EventId { get; set; }
        public string RatingTitle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Event
    {
        public string EventId { get; set; }
        public string AccountId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public int VisitorLimit { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string ImageSrc { get; set; }
    }
}

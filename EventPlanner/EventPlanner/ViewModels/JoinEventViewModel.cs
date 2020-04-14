using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class JoinEventViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public int VisitorLimit { get; set; }
        public string Description { get; set; }
        public string ImageSrc { get; set; }
    }
}

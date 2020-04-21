using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class CategoryEventsViewModel
    {
        public List<Event> Events { get; set; }
        public string CategoryName { get; set; }
        public string CategoryInfo { get; set; }
    }
}

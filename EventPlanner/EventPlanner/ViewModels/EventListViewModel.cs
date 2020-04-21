using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class EventListViewModel
    {
        public List<Event> Events { get; set; }
        public string Category { get; set; }
    }
}

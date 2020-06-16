using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Models;

namespace EventPlanner.ViewModels
{
    public class EventsForEmployeesViewModel
    {
        public List<Event> Events { get; set; }
        public int Pages { get; set; }
        public int PageNumber { get; set; }
    }
}

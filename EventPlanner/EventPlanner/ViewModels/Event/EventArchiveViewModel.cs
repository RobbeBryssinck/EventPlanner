using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class EventArchiveViewModel
    {
        public List<Event> Events { get; set; }
        public int Pages { get; set; }
        public int PageNumber { get; set; }
    }
}

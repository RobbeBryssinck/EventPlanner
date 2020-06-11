using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class EventsViewModel
    {
        public List<Event> Events { get; set; }
        public List<Categorie> Categories { get; set; }
        public int Pages { get; set; }
        public string SearchString { get; set; }
    }
}

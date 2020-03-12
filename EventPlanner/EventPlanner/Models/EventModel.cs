using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class EventModel
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
        
        public int StartEvent { get; set; }
        
        public int EndEvent { get; set; }

        public DateTime Date { get; set; }
    }
}

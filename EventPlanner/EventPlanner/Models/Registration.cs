using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int EventId { get; set; }
        public int AccountId { get; set; }
    }
}

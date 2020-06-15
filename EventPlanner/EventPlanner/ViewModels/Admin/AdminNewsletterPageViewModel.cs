using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class AdminNewsletterPageViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public bool IsSelected { get; set; }
        public string MailName { get; set; }
    }
}

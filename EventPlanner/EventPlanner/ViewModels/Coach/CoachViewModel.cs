using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class CoachViewModel
    {
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Email { get; set; }
        public string ImageSrc { get; set; }
        public int Pages { get; set; }
        public int PageNumber { get; set; }
        public List<Coach> Coaches { get; set; }
    }
}

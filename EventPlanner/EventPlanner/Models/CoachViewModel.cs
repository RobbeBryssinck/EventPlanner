using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class CoachViewModel
    {
        [Key]
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string ImageSrc { get; set; }
    }
}

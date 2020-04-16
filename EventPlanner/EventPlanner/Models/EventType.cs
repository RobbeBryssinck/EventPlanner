using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public enum EventType
    {
        [Display(Name = "Educatief")]
        Educational,

        [Display(Name = "Recreatie")]
        Recreation,
    }
}

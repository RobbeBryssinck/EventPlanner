using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public enum EventGroup
    {
        [Display(Name = "Iedereen")]
        Public,

        [Display(Name = "Rockstars IT medewerkers")]
        RockstarsEmployees,
    }
}

using EventPlanner.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class AdminCoachPageViewModel
    {
        public List<Coach> Coaches { get; set; }
    }
}

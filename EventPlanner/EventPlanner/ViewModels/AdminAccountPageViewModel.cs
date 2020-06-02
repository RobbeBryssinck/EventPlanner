using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Models;

namespace EventPlanner.ViewModels
{
    public class AdminAccountPageViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}

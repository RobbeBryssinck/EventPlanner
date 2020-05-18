using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class AdminAccountPageViewModel
    {
        public List<IdentityUser> Users { get; set; }
    }
}

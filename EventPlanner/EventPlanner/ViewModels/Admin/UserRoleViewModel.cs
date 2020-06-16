using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.ViewModels
{
    public class UserRoleViewModel
    {
        public List<UserRoleViewModel> model { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
        public int Pages { get; set; }
        public int PageNumber { get; set; }
    }
}

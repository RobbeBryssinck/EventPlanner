using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        private EventPlannerContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EventPlannerContext db)
        {
            this.db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var emp_data = db.Events.Where(f => f.Date > DateTime.Now && f.ForEmployees != EventGroup.RockstarsEmployees && f.hidden == false).OrderBy(e => e.Date).ToList().Take(6);
            HomeIndexViewModel model = new HomeIndexViewModel()
            {
                Events = emp_data
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ErrorUser()
        {
            return View("Error");
        }
    }
}

﻿using System;
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
            var emp_data = db.Events.Where(f => f.Date > DateTime.Now && f.ForEmployees != EventGroup.RockstarsEmployees).OrderBy(e => e.Date).ToList().Take(3);
            return View(emp_data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

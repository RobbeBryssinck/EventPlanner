﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        public IActionResult EventPage()
        {
            return View();
        }

        public IActionResult Event()
        {
            return View();
        }

    }
}
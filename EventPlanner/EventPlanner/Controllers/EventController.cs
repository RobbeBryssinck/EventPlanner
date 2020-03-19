using System;
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

        public ActionResult Create()
        {
            Event @event = new Event();

            try
            {

                UpdateModel(event);

                dinnerRepository.Add(event);
                dinnerRepository.Save();

                return RedirectToAction("Details", new { id = event.DinnerID });
            }
            catch
            {

                ModelState.AddRuleViolations(event.GetRuleViolations());

                return View(event);
            }
        }
    }
}
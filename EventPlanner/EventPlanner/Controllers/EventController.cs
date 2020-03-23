using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {

        private EventPlannerContext db;

        public EventController(EventPlannerContext db)
        {
            this.db = db;
        }

        public IActionResult EventPage()
        {
            return View();
        }

        public IActionResult EventSuccessPage()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            return View(new Event());
        }
        
        [HttpPost]
        public ActionResult CreateEvent(Event model)
        {

            if (ModelState.IsValid)
            {
                db.Events.Add(model);
                db.SaveChanges();
            }

            return View();
        }
    }
}
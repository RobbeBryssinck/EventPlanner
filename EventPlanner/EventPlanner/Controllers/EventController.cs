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

        public IActionResult EventPage(int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            return View(events[0]);
        }

        public IActionResult EventSuccessPage(Event model)
        {

            return View(model);
        }

        public IActionResult NotFound()
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

            return RedirectToAction("EventSuccessPage", model);
        }

        public IActionResult Events(string id)
        {
            List<Event> events = new List<Event>();

            if (!String.IsNullOrEmpty(id))
            {
                events = db.Events.Where(s => s.EventName.Contains(id)).ToList();
            }
            else
            {
                events = db.Events.ToList();
            }

            if (events.Count == 0)
            {
                return RedirectToAction("NotFound");
            }

            return View(events);
        }

        public IActionResult Guilds()
        {
            List<Event> events = db.Events.Where(s => s.EventType == "Guild").ToList();

            return View(events);
        }
    }
}
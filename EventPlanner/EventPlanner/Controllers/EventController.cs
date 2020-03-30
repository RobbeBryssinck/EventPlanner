﻿using System;
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

        public IActionResult Events()
        {
            List<Event> events = db.Events.ToList();
            return View(events);
        }
        public IActionResult EventsJoin(int id)
        {
            List<Event> events = db.Events.Where(x => x.EventId == id).ToList();
            return View(events[0]);
        }
    }
}
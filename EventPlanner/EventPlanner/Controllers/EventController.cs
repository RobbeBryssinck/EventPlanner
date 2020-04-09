﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {

        private EventPlannerContext db;
        private IWebHostEnvironment _environment;


        public EventController(EventPlannerContext db, IWebHostEnvironment environment)
        {
            this.db = db;
            this._environment = environment;
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

        public IActionResult EventFeedbackPage(int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            return View(events[0]);
        }

        public IActionResult CreateEvent()
        {
            return View(new EventViewModel());
        }

        public IActionResult EventNotFound()
        {
            return View();
        }

        public IActionResult ArchivedEvent(int eventID)
        {
            RatingEventViewModel ratingEventViewModel = new RatingEventViewModel();
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            Event currentEvent = events[0];

            List<Rating> ratings = db.Ratings.Where(x => x.EventId == eventID).ToList();

            ratingEventViewModel.Event = currentEvent;
            ratingEventViewModel.Ratings = ratings;
            return View(ratingEventViewModel);
        }

        /*

        [HttpPost]
        public IActionResult CreateEvent(Event model)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(model);
                db.SaveChanges();

                return View("EventImage");
            }
            else
            {
                return View("EventCreateFail");
            }
        }
        */

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            Event realmodel = new Event();
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images");
                foreach (var file in model.files)
                {
                    realmodel.ImageSrc = file.FileName;
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }


                realmodel.EventId = model.EventId;
                realmodel.EventName = model.EventName;
                realmodel.Date = model.Date;
                realmodel.VisitorLimit = model.VisitorLimit;
                realmodel.Description = model.Description;
                realmodel.Location = model.Location;
                realmodel.EventType = model.EventType;
                realmodel.Email = model.Email;

                db.Events.Add(realmodel);
                db.SaveChanges();
                return View("EventCreationSucces");
            }

            else
                return View("EventCreateFail");
        }


        public IActionResult Events(string id)
        {
            //TODO: change List to IEnumerable or IReadOnly?
            List<Event> events = new List<Event>();

            if (!String.IsNullOrEmpty(id))
            {
                events = db.Events.Where(s => s.EventName.Contains(id)).ToList();
            }
            else
            {
                events = db.Events.Where(s => s.Date > DateTime.Now).ToList();
            }

            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }

            return View(events);
        }

        public IActionResult Educational()
        {
            List<Event> events = db.Events.Where(s => s.EventType == "Educational" && s.Date > DateTime.Now).ToList();

            return View(events);
        }

        public IActionResult Recreational()
        {
            List<Event> events = db.Events.Where(s => s.EventType == "Recreational" && s.Date > DateTime.Now).ToList();

            return View(events);
        }
        public IActionResult EventArchive()
        {
            List<Event> events = db.Events.Where(s => s.Date < DateTime.Now).ToList();

            return View(events);
        }
        public IActionResult EventsJoin(int id)
        {
            List<Event> events = db.Events.Where(x => x.EventId == id).ToList();
            return View(events[0]);
        }
    }
}
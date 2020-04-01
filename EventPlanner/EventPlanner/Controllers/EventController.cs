using System;
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

        public IActionResult CreateEvent()
        {
            return View(new EventViewModel());
        }

        public IActionResult EventNotFound()
        {
            return View();
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
                return RedirectToAction("EventNotFound");
            }

            return View(events);
        }

        public IActionResult Guilds()
        {
            List<Event> events = db.Events.Where(s => s.EventType == "Guild").ToList();

            return View(events);
        }

        public IActionResult Chapter()
        {
            List<Event> events = db.Events.Where(s => s.EventType == "Chapter").ToList();

            return View(events);
        }
        public IActionResult EventsJoin(int id)
        {
            List<Event> events = db.Events.Where(x => x.EventId == id).ToList();
            return View(events[0]);
        }
    }
}
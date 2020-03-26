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
        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "Images");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return View("CreateEvent");
        }


        public IActionResult Events()
        {
            List<Event> events = db.Events.ToList();
            return View(events);
        }
    }
}
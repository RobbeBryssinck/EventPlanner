using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EventPlanner.Controllers
{
    public class AdminController : Controller
    {
        private EventPlannerContext db;
        private IWebHostEnvironment _environment;


        public AdminController(EventPlannerContext db, IWebHostEnvironment environment)
        {
            this.db = db;
            this._environment = environment;
        }

        public IActionResult AdminAccountPage()
        {
            return View();
        }
        public IActionResult AdminCoachPage()
        {
            return View();
        }

        public IActionResult AdminEventPage(string id)
        {
            //TODO: change List to IEnumerable or IReadOnly?
            List<Event> events = new List<Event>();

            if (!String.IsNullOrEmpty(id))
            {
                events = db.Events.Where(s => s.EventName.Contains(id)).ToList();
            }
            else
            {
                events = db.Events.Where(s => s.Date > DateTime.Now && s.ForEmployees == EventGroup.Public).ToList();
            }

            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }

            return View(events);
        }
    }
}
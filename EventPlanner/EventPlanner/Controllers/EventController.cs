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
            Event model = events[0];
            EventViewModel realmodel = new EventViewModel();

            var Participants = db.Registrations.Where(b => b.EventId == model.EventId).Count();
            realmodel.EventId = model.EventId;
            realmodel.EventName = model.EventName;
            realmodel.Date = model.Date;
            realmodel.ImageSrc = model.ImageSrc;
            realmodel.VisitorLimit = model.VisitorLimit;
            realmodel.Description = model.Description;
            realmodel.Location = model.Location;
            realmodel.EventType = model.EventType;
            realmodel.Email = model.Email;
            realmodel.Visitors = Participants;

            return View(realmodel);
        }

        public IActionResult EventSuccessPage(Event model)
        {

            return View(model);
        }

        public IActionResult EventCategories()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateCategorie(Categorie model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
                db.SaveChanges();
                return View("FeedbackSubmitted");
            }

            else
                return View("FeedbackCreateFail");
        }

        public IActionResult EventFeedbackPage(int eventID)
        {
            Rating rating = new Rating();
            rating.EventId = eventID;

            return View(rating);
        }

        public IActionResult CreateEvent()
        {
            EventViewModel model = new EventViewModel();
            model.Categories = db.Categories.ToList();
            return View(model);
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

        public IActionResult DeleteFeedbackPage(int ratingID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            return View(ratings[0]);
        }

        public IActionResult DeleteFeedback(int ratingID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            db.Ratings.Remove(ratings[0]);
            db.SaveChanges();
            return RedirectToAction("DeleteFeedbackComplete");
        }

        public IActionResult DeleteFeedbackComplete()
        {
            return View();
        }

        public IActionResult ChangeEventPage(int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            Event model = events[0];
            EventViewModel realmodel = new EventViewModel();
            realmodel.Categories = db.Categories.ToList();

            realmodel.EventId = model.EventId;
            realmodel.EventName = model.EventName;
            realmodel.Date = model.Date;
            realmodel.VisitorLimit = model.VisitorLimit;
            realmodel.Description = model.Description;
            realmodel.Location = model.Location;
            realmodel.EventType = model.EventType;
            realmodel.Email = model.Email;

            return View(realmodel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEventPage(EventViewModel model)
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
                realmodel.Location = model.Location.Replace(" ", String.Empty);
                realmodel.EventType = model.EventType;
                realmodel.Email = model.Email;


                List<Event> events = db.Events.Where(x => x.EventId == model.EventId).ToList();
                Event oldEvent = events[0];
                db.Entry(oldEvent).CurrentValues.SetValues(realmodel);
                db.SaveChanges();
                return RedirectToAction("EventPage", new { realmodel.EventId });
            }
            else
            {
                return View("EventCreateFail");
            }
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
        public IActionResult CreateFeedback(Rating rating, int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            Event currentEvent = events[0];

            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return View("FeedbackSubmitted");
            }

            else
                return View("FeedbackCreateFail");
        }


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
                realmodel.Location = model.Location.Replace(" ", String.Empty);
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
            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }
            return View(events);
        }

        public IActionResult Recreation()
        {
            List<Event> events = db.Events.Where(s => s.EventType == "Recreation" && s.Date > DateTime.Now).ToList();
            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }
            return View(events);
        }
        public IActionResult EventArchive()
        {
            List<Event> events = db.Events.Where(s => s.Date < DateTime.Now).ToList();
            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }
            return View(events);
        }

        [HttpGet]
        public IActionResult EventJoin(int eventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventId).ToList();
            Event joinEvent = events[0];
            EventJoinPageViewModel joinEventViewModel = new EventJoinPageViewModel();

            joinEventViewModel.EventId = joinEvent.EventId;
            joinEventViewModel.EventName = joinEvent.EventName;
            joinEventViewModel.Date = joinEvent.Date;
            joinEventViewModel.VisitorLimit = joinEvent.VisitorLimit;
            joinEventViewModel.Description = joinEvent.Description;
            joinEventViewModel.ImageSrc = joinEvent.ImageSrc;

            return View(joinEventViewModel);
        }

        [HttpPost]
        public IActionResult EventJoin(EventJoinPageViewModel model)
        {
            Registration registration = new Registration();
            List<Account> accounts = db.Accounts.Where(x => x.UserName == model.Username).ToList();
            Account account = accounts[0];

            registration.AccountId = account.AccountId;
            registration.EventId = model.EventId;

            db.Registrations.Add(registration);
            db.SaveChanges();

            return View("RegistrationSucceeded");
        }

        public IActionResult DeleteEventPage(int EventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == EventId).ToList();
            return View(events[0]);
        }

        public IActionResult DeleteEvent(int EventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == EventId).ToList();
            db.Events.Remove(events[0]);
            db.SaveChanges();
            return RedirectToAction("DeleteEventComplete");
        }

        public IActionResult DeleteEventComplete()
        {
            return View();
        }
    }
}
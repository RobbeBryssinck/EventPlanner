using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {

        private EventPlannerContext db;
        private IWebHostEnvironment _environment;
        private readonly long _fileSizeLimit;


        public EventController(EventPlannerContext db, IWebHostEnvironment environment, IConfiguration config)
        {
            this.db = db;
            this._environment = environment;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        public IActionResult EventPage(int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            if (events.Count > 0)
            {
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
                realmodel.CategoryId = model.CategoryId;
                realmodel.Email = model.Email;
                realmodel.Visitors = Participants;

                return View(realmodel);
            }
            else
            {
                return View("PageNotFoundError");
            }
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
                return View("CategoryCreated");
            }

            else
                return View("CategoryFailed");
        }
        public IActionResult CategoryDeletePage(int CategoryID)
        {
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();
            return View(categories[0]);
        }
        public IActionResult DeleteCategory(int CategoryID)
        {
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();
            db.Categories.Remove(categories[0]);
            db.SaveChanges();
            return View("CategoryDeleted");
        }

        public IActionResult EventFeedbackPage(int eventID)
        {
            Rating rating = new Rating();
            rating.EventId = eventID;

            return View(rating);
        }

        public IActionResult EventCreate()
        {
            EventViewModel model = new EventViewModel();
            model.Categories = db.Categories.ToList();
            return View(model);
        }

        public IActionResult EventNotFound()
        {
            return View();
        }

        public IActionResult EventChangeFail()
        {
            return View();
        }

        public IActionResult EventArchived(int eventID)
        {
            EventRatingViewModel ratingEventViewModel = new EventRatingViewModel();
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            if (events.Count > 0)
            {
                Event currentEvent = events[0];

                List<Rating> ratings = db.Ratings.Where(x => x.EventId == eventID).ToList();

                ratingEventViewModel.Event = currentEvent;
                ratingEventViewModel.Ratings = ratings;
                return View(ratingEventViewModel);
            }
            else
            {
                return View("PageNotFoundError");
            }
        }

        public IActionResult EventDeleteFeedbackPage(int ratingID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            EventFeedbackDeleteViewModel model = new EventFeedbackDeleteViewModel()
            {
                RatingId = ratings[0].RatingId,
                RatingTitle = ratings[0].RatingTitle
            };
            return View(model);
        }

        public IActionResult DeleteFeedback(int ratingID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            db.Ratings.Remove(ratings[0]);
            db.SaveChanges();
            return RedirectToAction("EventDeleteFeedbackComplete");
        }

        public IActionResult EventDeleteFeedbackComplete()
        {
            return View();
        }

        public IActionResult EventChangePage(int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            Event model = events[0];
            EventChangePageViewModel realmodel = new EventChangePageViewModel();
            realmodel.Categories = db.Categories.ToList();

            realmodel.EventId = model.EventId;
            realmodel.EventName = model.EventName;
            realmodel.Date = model.Date;
            realmodel.VisitorLimit = model.VisitorLimit;
            realmodel.Description = model.Description;
            realmodel.Location = model.Location;
            realmodel.ImageSrc = model.ImageSrc;
            realmodel.Email = model.Email;


            return View(realmodel);
        }

        [HttpPost]
        public async Task<IActionResult> EventChangePage(EventChangePageViewModel model)
        {
            Event realmodel = new Event();
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images/Events");
                if (model.files != null)
                {
                    foreach (var file in model.files)
                    {
                        if (file.Length > 0 && file.Length < _fileSizeLimit)
                        {
                            realmodel.ImageSrc = file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            return View("EventCreateFail");
                        }
                    }
                }
                else
                {
                    return View("EventCreateFail");
                }

                realmodel.EventId = model.EventId;
                realmodel.EventName = model.EventName;
                realmodel.Date = model.Date;
                realmodel.VisitorLimit = model.VisitorLimit;
                realmodel.Description = model.Description;
                realmodel.Location = model.Location.Replace(" ", String.Empty);
                realmodel.ForEmployees = model.ForEmployees;
                if (model.files == null)
                {
                    realmodel.ImageSrc = model.ImageSrc;
                }
                realmodel.Email = model.Email;


                List<Event> events = db.Events.Where(x => x.EventId == model.EventId).ToList();
                Event oldEvent = events[0];
                db.Entry(oldEvent).CurrentValues.SetValues(realmodel);
                db.SaveChanges();
                return RedirectToAction("EventPage", new { realmodel.EventId });
            }
            else
            {
                return View("EventChangeFail");
            }
        }

        [HttpPost]
        public IActionResult CreateFeedback(Rating rating, int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            Event currentEvent = events[0];

            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return View("EventFeedbackSubmitted");
            }

            else
                return View("EventFeedbackCreateFail");
        }


        [HttpPost]
        public async Task<IActionResult> EventCreate(EventViewModel model)
        {
            Event realmodel = new Event();
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images/Events");
                foreach (var file in model.files)
                {
                    if (file.Length > 0 && file.Length < _fileSizeLimit)
                    {
                        realmodel.ImageSrc = file.FileName;
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        return View("EventCreateFail");
                    }

                }


                realmodel.EventId = model.EventId;
                realmodel.EventName = model.EventName;
                realmodel.Date = model.Date;
                realmodel.VisitorLimit = model.VisitorLimit;
                realmodel.Description = model.Description;
                realmodel.Location = model.Location.Replace(" ", String.Empty);
                realmodel.CategoryId = model.CategoryId;
                realmodel.Email = model.Email;
                realmodel.ForEmployees = model.ForEmployees;

                db.Events.Add(realmodel);
                db.SaveChanges();
                return View("EventCreationSucces");
            }

            else
            {
                model.Categories = db.Categories.ToList();
                return View(model);
            }

        }

        public IActionResult Categories()
        {
            List<Categorie> categories = db.Categories.ToList();

            CategoriesViewModel model = new CategoriesViewModel();
            model.Categories = new List<Categorie>();
            foreach (Categorie category in categories)
            {
                model.Categories.Add(category);
            }
            return View(model);
        }

        public IActionResult CategoryPage(int CategoryID)
        {
            CategoryEventsViewModel model = new CategoryEventsViewModel();
            model.Events = db.Events.Where(s => s.CategoryId == CategoryID && s.Date > DateTime.Now).ToList();
            List<Categorie> categories = db.Categories.Where(s => s.CategorieId == CategoryID).ToList();
            if (categories.Count > 0)
            {
                model.CategoryInfo = categories[0].Info;
                model.CategoryName = categories[0].CategorieName;
                return View(model);
            }
            else
            {
                return View("PageNotFoundError");
            }
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
                events = db.Events.Where(s => s.Date > DateTime.Now && s.ForEmployees == EventGroup.Public).ToList();
            }

            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }

            EventsViewModel model = new EventsViewModel()
            {
                Events = events
            };

            return View(model);
        }

        public IActionResult EventArchive()
        {
            List<Event> events = db.Events.Where(s => s.Date < DateTime.Now).ToList();
            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }

            EventArchiveViewModel model = new EventArchiveViewModel()
            {
                Events = events
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult EventsForEmployees()
        {
            List<Event> events = db.Events.Where(s => s.Date > DateTime.Now && s.ForEmployees == EventGroup.RockstarsEmployees).ToList();
            EventsForEmployeesViewModel model = new EventsForEmployeesViewModel()
            {
                Events = events
            };
            return View(model);
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
            //test commit

            registration.AccountId = account.AccountId;
            registration.EventId = model.EventId;

            db.Registrations.Add(registration);
            db.SaveChanges();

            return View("EventRegistrationSucceeded");
        }

        public IActionResult EventDeletePage(int EventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == EventId).ToList();
            EventDeleteViewModel model = new EventDeleteViewModel()
            {
                EventId = events[0].EventId,
                EventName = events[0].EventName
            };
            return View(model);
        }

        public IActionResult DeleteEvent(int EventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == EventId).ToList();
            db.Events.Remove(events[0]);
            db.SaveChanges();
            return RedirectToAction("EventDeleteComplete");
        }

        public IActionResult EventDeleteComplete()
        {
            return View();
        }
        public IActionResult CategoryChangePage(int CategoryID)
        {
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();
            Categorie model = categories[0];
            CategoriesViewModel realmodel = new CategoriesViewModel();
            realmodel.CategorieId = model.CategorieId;
            realmodel.CategorieName = model.CategorieName;
            realmodel.Info = model.Info;

            return View(realmodel);
        }
        public IActionResult ChangeCategory(CategoriesViewModel model)
        {
            Categorie realmodel = new Categorie();
            if (ModelState.IsValid)
            {
                realmodel.CategorieId = model.CategorieId;
                realmodel.CategorieName = model.CategorieName;
                realmodel.Info = model.Info;

                List<Categorie> categories = db.Categories.Where(x => x.CategorieId == model.CategorieId).ToList();
                Categorie oldCategory = categories[0];
                db.Entry(oldCategory).CurrentValues.SetValues(realmodel);
                db.SaveChanges();
                return RedirectToAction("AdminCategoryPage", "Admin");
            }
            else
            {
                return Content("Het werkt niet");
            }
        }
    }
}
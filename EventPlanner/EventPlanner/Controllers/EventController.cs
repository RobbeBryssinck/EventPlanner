using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {

        private EventPlannerContext db;
        private IWebHostEnvironment _environment;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private readonly long _fileSizeLimit;
        private string[] permittedExtensions = { ".png", ".jpg", ".jpeg" };

        public EventController(EventPlannerContext db, IWebHostEnvironment environment, IConfiguration config, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this._environment = environment;
            this.roleManager = roleManager;
            this.userManager = userManager;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        public async Task<IActionResult> EventPage(int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            if (events.Count > 0)
            {
                Event model = events[0];
                EventViewModel realmodel = new EventViewModel();
              //  var Participants = db.Registrations.Where(b => b.EventId == model.EventId).Count();
                realmodel.EventId = model.EventId;
                realmodel.EventName = model.EventName;
                realmodel.Date = model.Date;
                realmodel.ImageSrc = model.ImageSrc;
                realmodel.VisitorLimit = model.VisitorLimit;
                realmodel.Description = model.Description;
                realmodel.Location = model.Location;
                realmodel.CategoryId = model.CategoryId;
                realmodel.Email = model.Email;
                realmodel.Visitors = model.Visitors;
                realmodel.TotalVisitors = model.TotalVisitors;
                

                if (User.Identity.IsAuthenticated)
                {
                    var user = await userManager.GetUserAsync(User);
                    List<Registration> registrations = db.Registrations.Where(x => x.AccountId == user.Id && x.EventId == eventID).ToList();
                    if (registrations.Count == 0)
                        realmodel.Registered = false;
                    else
                        realmodel.Registered = true;
                }
                else
                    realmodel.Registered = false;

                if (model.hidden == false || User.IsInRole("Admin") && model.hidden == true)
                {
                    return View(realmodel);
                }
                else
                {
                    return View("PageNotFoundError");
                }
            }
            else
            {
                return View("PageNotFoundError");
            }
        }


        public IActionResult EventCategories()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult CategoryDeletePage(int CategoryID)
        {
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();
            return View(categories[0]);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(int CategoryID)
        {
            List<Event> events = db.Events.Where(x => x.CategoryId == CategoryID).ToList();
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();

            foreach (var model in events)
            {
                model.hidden = true;
                db.Events.Attach(model);
                db.Entry(model).Property(x => x.hidden).IsModified = true;
                db.SaveChanges();
            }

            foreach (var model in categories)
            {
                model.hidden = true;
                db.Categories.Attach(model);
                db.Entry(model).Property(x => x.hidden).IsModified = true;
                db.SaveChanges();
            }

            return View("CategoryDeleted");
        }

        [Authorize(Roles = "Rockstar, User")]
        public IActionResult EventFeedbackPage(int eventID)
        {
            Rating rating = new Rating();
            rating.EventId = eventID;

            return View(rating);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EventCreate()
        {
            EventViewModel model = new EventViewModel();
            model.Categories = db.Categories.Where(x => x.hidden == false).ToList();
            return View(model);
        }

        public IActionResult EventNotFound()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EventChangeFail()
        {
            return View();
        }

        public IActionResult EventArchived(int eventID)
        {
            EventRatingViewModel ratingEventViewModel = new EventRatingViewModel();
            List<Event> events = db.Events.Where(x => x.EventId == eventID && x.hidden == false).ToList();
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

        [Authorize(Roles = "Rockstar, User")]
        public IActionResult EventArchivedForEmployees(int eventID)
        {
            EventRatingViewModel ratingEventViewModel = new EventRatingViewModel();
            List<Event> events = db.Events.Where(x => x.EventId == eventID && x.hidden == false).ToList();
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

        [Authorize(Roles = "User")]
        public IActionResult EventDeleteFeedbackPage(int ratingID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            EventFeedbackDeleteViewModel model = new EventFeedbackDeleteViewModel()
            {
                RatingId = ratings[0].RatingId,
                RatingTitle = ratings[0].RatingTitle,
                EventId = ratings[0].EventId
            };
            return View(model);
        }

        [Authorize(Roles = "Rockstar, User")]
        public IActionResult DeleteFeedback(int ratingID, int eventID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            db.Ratings.Remove(ratings[0]);
            db.SaveChanges();
            return RedirectToAction("EventArchived", "Event", new { eventID = eventID });
        }

        [Authorize(Roles = "Rockstar, User")]
        public IActionResult EventDeleteFeedbackComplete()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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
            realmodel.ForEmployees = model.ForEmployees;
            realmodel.CategoryId = model.CategoryId;

            return View(realmodel);
        }

        [Authorize(Roles = "Admin")]
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
                        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                        if (file.Length > 0 && file.Length < _fileSizeLimit && permittedExtensions.Contains(ext))
                        {
                            realmodel.ImageSrc = file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            model.Categories = db.Categories.ToList();
                            ModelState.AddModelError("files", "Het bestand is ongeldig");
                            return View(model);
                        }
                    }
                }

                realmodel.EventId = model.EventId;
                realmodel.EventName = model.EventName;
                realmodel.Date = model.Date;
                realmodel.VisitorLimit = model.VisitorLimit;
                realmodel.Description = model.Description;
                realmodel.Location = model.Location.Replace(" ", String.Empty);
                realmodel.ForEmployees = model.ForEmployees;
                realmodel.CategoryId = model.CategoryId;
                if (model.files == null)
                {
                    realmodel.ImageSrc = model.ImageSrc;
                }
                realmodel.Email = model.Email;


                List<Event> events = db.Events.Where(x => x.EventId == model.EventId).ToList();
                Event oldEvent = events[0];
                db.Entry(oldEvent).CurrentValues.SetValues(realmodel);
                db.SaveChanges();
                return RedirectToAction("AdminEventPage", "Admin", new { realmodel.EventId });
            }
            else
            {
                model.Categories = db.Categories.ToList();

                return View(model);
            }
        }

        [Authorize(Roles = "Rockstar, User")]
        [HttpPost]
        public IActionResult CreateFeedback(Rating rating, int eventID)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventID).ToList();
            Event currentEvent = events[0];

            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("EventArchived", "Event", new { eventID = eventID });
            }

            else
                return View("EventFeedbackCreateFail");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EventCreate(EventViewModel model)
        {
            Event realmodel = new Event();
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images/Events");
                foreach (var file in model.files)
                {
                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (file.Length > 0 && file.Length < _fileSizeLimit && permittedExtensions.Contains(ext))
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
                realmodel.TotalVisitors = model.VisitorLimit;

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
            List<Categorie> categories = db.Categories.Where(x => x.hidden == false && x.hidden == false).ToList();

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
            model.Events = db.Events.Where(s => s.CategoryId == CategoryID && s.Date > DateTime.Now && s.hidden == false).ToList();
            List<Categorie> categories = db.Categories.Where(s => s.CategorieId == CategoryID && s.hidden == false).ToList();
            if (categories.Count > 0)
            {
                /*
                foreach (Event e in model.Events)
                {
                    var Participants = db.Registrations.Where(b => b.EventId == e.EventId).Count();
                    e.Visitors = Participants;
                }
                */

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
            List<Categorie> categories = db.Categories.Where(x => x.hidden == false && x.hidden == false).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                events = db.Events.Where(s => s.EventName.Contains(id) && s.Date > DateTime.Now && s.ForEmployees == EventGroup.Public && s.hidden == false).ToList();
            }
            else
            {
                events = db.Events.Where(s => s.Date > DateTime.Now && s.ForEmployees == EventGroup.Public && s.hidden == false).ToList();
            }

            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }
            /*
            foreach (var models in events)
            {
                var Participants = db.Registrations.Where(b => b.EventId == models.EventId).Count();
                models.Visitors = Participants;
            }
            */
            EventsViewModel model = new EventsViewModel()
            {
                Events = events,
                Categories = categories
            };
            return View(model);
        }
        public IActionResult EventArchive()
        {
            List<Event> events = db.Events.Where(s => s.Date < DateTime.Now && s.ForEmployees == EventGroup.Public && s.hidden == false).ToList();
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
        [Authorize(Roles = "Rockstar, Admin")]
        public IActionResult EventArchiveForEmployees()
        {
            List<Event> events = db.Events.Where(s => s.Date < DateTime.Now && s.ForEmployees == EventGroup.RockstarsEmployees && s.hidden == false).ToList();
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
        [Authorize(Roles = "Admin, Rockstar")]
        [HttpGet]
        public IActionResult EventsForEmployees()
        {
            List<Event> events = db.Events.Where(s => s.Date > DateTime.Now && s.ForEmployees == EventGroup.RockstarsEmployees && s.hidden == false).ToList();
            /*
            foreach (var item in events)
            {
                var Participants = db.Registrations.Where(b => b.EventId == item.EventId).Count();
                item.Visitors = Participants;
            }
            */
            EventsForEmployeesViewModel model = new EventsForEmployeesViewModel()
            {
                Events = events
            };

            return View(model);
        }

        [Authorize(Roles = "Rockstar, User")]
        [HttpPost]
        public async Task<IActionResult> EventJoin(int eventId)
        {
            Registration registration = new Registration();
            var user = await userManager.GetUserAsync(User);

            registration.AccountId = user.Id;
            registration.EventId = eventId;

            db.Registrations.Add(registration);
            db.SaveChanges();
            UpdateParticipants(eventId);
            return View("EventRegistrationSucceeded");
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEvent(int EventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == EventId).ToList();
            foreach (var model in events)
            {
                model.hidden = true;
                db.Events.Attach(model);
                db.Entry(model).Property(x => x.hidden).IsModified = true;
                db.SaveChanges();
            }       
            return RedirectToAction("EventDeleteComplete");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EventDeleteComplete()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CategoryChangePage(int CategoryID)
        {
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID && x.hidden == false).ToList();
            Categorie model = categories[0];
            CategoriesViewModel realmodel = new CategoriesViewModel();
            realmodel.CategorieId = model.CategorieId;
            realmodel.CategorieName = model.CategorieName;
            realmodel.Info = model.Info;

            return View(realmodel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ChangeCategory(CategoriesViewModel model)
        {
            Categorie realmodel = new Categorie();
            if (ModelState.IsValid)
            {
                realmodel.CategorieId = model.CategorieId;
                realmodel.CategorieName = model.CategorieName;
                realmodel.Info = model.Info;

                List<Categorie> categories = db.Categories.Where(x => x.CategorieId == model.CategorieId && x.hidden == false).ToList();
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
        [Authorize(Roles = "Rockstar, User")]
        public async Task<IActionResult> SignOutOfEvent(int eventId)
        {
            var user = await userManager.GetUserAsync(User);
            Registration registration = db.Registrations.Where(x => x.AccountId == user.Id && x.EventId == eventId).FirstOrDefault();
            db.Registrations.Remove(registration);
            db.SaveChanges();
            List<Event> events = db.Events.Where(x => x.EventId == eventId).ToList();
            UpdateParticipants(eventId);
            return RedirectToAction("EventRegistered", "Account");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EventDeleted()
        {
            EventsViewModel realmodel = new EventsViewModel();
            List<Event> events = db.Events.Where(x => x.hidden == true).ToList();
            if (events.Count == 0)
            {
                return RedirectToAction("EventNotFound");
            }
            realmodel.Events = events;
            return View(realmodel);
        }
        private void UpdateParticipants(int eventId)
        {
            List<Event> events = db.Events.Where(x => x.EventId == eventId).ToList();
            foreach (var model in events)
            {
                var Participants = db.Registrations.Where(b => b.EventId == model.EventId).Count();
                model.TotalVisitors = model.VisitorLimit - Participants;
                model.Visitors = Participants;
                db.Events.Attach(model);
                db.Entry(model).Property(x => x.TotalVisitors).IsModified = true;
                db.Entry(model).Property(x => x.Visitors).IsModified = true;
                db.SaveChanges();
            }
        }


    }
}
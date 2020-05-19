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
        private UserManager<IdentityUser> userManager;
        private readonly long _fileSizeLimit;
        private string[] permittedExtensions = { ".png", ".jpg", ".jpeg" };

        public EventController(EventPlannerContext db, IWebHostEnvironment environment, IConfiguration config, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this._environment = environment;
            this.roleManager = roleManager;
            this.userManager = userManager;
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
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();
            db.Categories.Remove(categories[0]);
            db.SaveChanges();
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
            model.Categories = db.Categories.ToList();
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

        [Authorize(Roles = "Rockstar, User")]
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

        [Authorize(Roles = "Rockstar, User")]
        public IActionResult DeleteFeedback(int ratingID)
        {
            List<Rating> ratings = db.Ratings.Where(x => x.RatingId == ratingID).ToList();
            db.Ratings.Remove(ratings[0]);
            db.SaveChanges();
            return RedirectToAction("EventDeleteFeedbackComplete");
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
                return View("EventFeedbackSubmitted");
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
            foreach(var models in events)
            {
                var Participants = db.Registrations.Where(b => b.EventId == models.EventId).Count();
                models.Visitors = Participants;
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
        [Authorize(Roles = "Admin, Rockstar")]
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

        [Authorize(Roles = "Rockstar, User")]
        [HttpPost]
        public async Task<IActionResult> EventJoin(string userName, int eventId)
        {
            Registration registration = new Registration();
            IdentityUser user = await userManager.FindByNameAsync(userName);

            registration.AccountId = user.Id;
            registration.EventId = eventId;

            db.Registrations.Add(registration);
            db.SaveChanges();

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
            db.Events.Remove(events[0]);
            db.SaveChanges();
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
            List<Categorie> categories = db.Categories.Where(x => x.CategorieId == CategoryID).ToList();
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
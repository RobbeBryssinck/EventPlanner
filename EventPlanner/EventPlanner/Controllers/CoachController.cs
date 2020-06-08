using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Controllers
{
    public class CoachController : Controller
    {
        private EventPlannerContext db;
        private IWebHostEnvironment _environment;
        private readonly long _fileSizeLimit;
        private string[] permittedExtensions = { ".png", ".jpg", ".jpeg" };

        public CoachController(EventPlannerContext db, IWebHostEnvironment environment, IConfiguration config)
        {
            this.db = db;
            this._environment = environment;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        [Authorize(Roles = "Admin, Rockstar")]
        public IActionResult Coaches()
        {
            return View(db.Coaches.ToList());
        }

        [Authorize(Roles = "Admin, Rockstar")]
        public IActionResult CoachPage(int coachID)
        {
            List<Coach> coaches = db.Coaches.Where(x => x.CoachId == coachID).ToList();
            if (coaches.Count > 0)
            {
                Coach coach = coaches[0];
                CoachViewModel model = new CoachViewModel()
                {
                    CoachId = coach.CoachId,
                    Name = coach.Name,
                    Info = coach.Info,
                    Email = coach.Email,
                    ImageSrc = coach.ImageSrc
                };
                return View(model);
            }
            else
            {
                return View("PageNotFoundError");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CoachAdd()
        {
            return View(new CoachAddViewModel());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CoachAdd(CoachAddViewModel model)
        {
            Coach realmodel = new Coach();

            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images/Coaches");
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
                            return View("CoachAddFail");
                        }
                    }
                }



                realmodel.CoachId = model.CoachId;
                realmodel.Name = model.Name;
                realmodel.Info = model.Info;
                realmodel.Email = model.Email;

                db.Coaches.Add(realmodel);
                db.SaveChanges();
                return View("CoachAddSucces");
            }
            else
                return View("CoachAddFail");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CoachDeletePage(int coachID)
        {
            List<Coach> coaches = db.Coaches.Where(x => x.CoachId == coachID).ToList();
            Coach coach = coaches[0];
            CoachDeleteViewModel model = new CoachDeleteViewModel()
            {
                CoachId = coach.CoachId,
                Name = coach.Name
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCoach(int coachID)
        {
            List<Coach> coaches = db.Coaches.Where(x => x.CoachId == coachID).ToList();
            db.Coaches.Remove(coaches[0]);
            db.SaveChanges();
            return RedirectToAction("CoachDeleteComplete");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CoachDeleteComplete()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ChangeCoachPage(int coachID)
        {
            List<Coach> coaches = db.Coaches.Where(x => x.CoachId == coachID).ToList();
            Coach model = coaches[0];
            CoachAddViewModel realmodel = new CoachAddViewModel();

            realmodel.CoachId = model.CoachId;
            realmodel.Name = model.Name;
            realmodel.Info = model.Info;
            realmodel.Email = model.Email;
            realmodel.ImageSrc = model.ImageSrc;


            return View(realmodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangeCoachPage(CoachAddViewModel model)
        {
            Coach realmodel = new Coach();
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images/Coaches");
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
                            return View("ChangeCoachFail");
                        }
                    }
                }


                realmodel.CoachId = model.CoachId;
                realmodel.Name = model.Name;
                realmodel.Info = model.Info;
                realmodel.Email = model.Email;
                if (model.files == null)
                {
                    realmodel.ImageSrc = model.ImageSrc;
                }
                realmodel.Email = model.Email;


                List<Coach> coaches = db.Coaches.Where(x => x.CoachId == model.CoachId).ToList();
                Coach oldCoach = coaches[0];
                db.Entry(oldCoach).CurrentValues.SetValues(realmodel);
                db.SaveChanges();
                return RedirectToAction("CoachPage", new { realmodel.CoachId });
            }
            else
            {
                return View("ChangeCoachFail");
            }
        }
    }
}
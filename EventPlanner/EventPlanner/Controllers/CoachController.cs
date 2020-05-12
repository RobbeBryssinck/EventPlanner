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

namespace EventPlanner.Controllers
{
    public class CoachController : Controller
    {
        private EventPlannerContext db;
        private IWebHostEnvironment _environment;

        public CoachController(EventPlannerContext db, IWebHostEnvironment environment)
        {
            this.db = db;
            this._environment = environment;
        }

        public IActionResult Coaches()
        {
            return View(db.Coaches.ToList());
        }

        public IActionResult CoachPage(int coachID)
        {
            List<Coach> coaches = db.Coaches.Where(x => x.CoachId == coachID).ToList();
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

        public IActionResult CoachAdd()
        {
            return View(new CoachAddViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CoachAdd(CoachAddViewModel model)
        {
            Coach realmodel = new Coach();

            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "Images/Coaches");
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

        public IActionResult DeleteCoach(int coachID)
        {
            List<Coach> coaches = db.Coaches.Where(x => x.CoachId == coachID).ToList();
            db.Coaches.Remove(coaches[0]);
            db.SaveChanges();
            return RedirectToAction("CoachDeleteComplete");
        }

        public IActionResult CoachDeleteComplete()
        {
            return View();
        }
    }
}
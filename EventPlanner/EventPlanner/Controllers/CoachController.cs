using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using EventPlanner.Data;
using EventPlanner.Models;

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
            return View(coaches[0]);
        }

        public IActionResult AddCoach()
        {
            return View(new AddCoachViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCoach(AddCoachViewModel model)
        {
            Coach realmodel = new Coach();

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

                realmodel.CoachId = model.CoachId;
                realmodel.Name = model.Name;
                realmodel.Info = model.Info;
                realmodel.Email = model.Email;

                db.Coaches.Add(realmodel);
                db.SaveChanges();
                return View("AddCoachSuccess");
            }
            else
                return View("AddCoachFail");
        }
    }
}
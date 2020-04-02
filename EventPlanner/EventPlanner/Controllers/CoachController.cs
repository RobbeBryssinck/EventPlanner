using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class CoachController : Controller
    {
        private EventPlannerContext db;

        public CoachController(EventPlannerContext db)
        {
            this.db = db;
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
    }
}
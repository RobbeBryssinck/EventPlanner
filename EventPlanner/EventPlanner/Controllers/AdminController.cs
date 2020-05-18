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
            AdminAccountPageViewModel model = new AdminAccountPageViewModel();
            model.Users = db.Users.ToList();
            return View(model);
        }
        public IActionResult AdminCoachPage()
        {
            return View(db.Coaches.ToList());
        }
        public IActionResult AdminEventPage()
        {
            AdminEventPageViewModel model = new AdminEventPageViewModel();
            model.Events = db.Events.ToList();
            model.Categories = db.Categories.ToList();
            return View(model);
        }
        public IActionResult AdminCategoryPage()
        {
            return View(db.Categories.ToList());
        }


    }
}
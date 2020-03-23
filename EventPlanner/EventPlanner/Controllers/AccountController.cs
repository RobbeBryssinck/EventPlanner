using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class AccountController : Controller
    {
        private EventPlannerContext db;

        public AccountController(EventPlannerContext db)
        {
            this.db = db;
        }

        public IActionResult LoginPage()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // login logic
            if (model.Username == "succeed")
                return RedirectToAction("LoginSucceeded");
            else
                return RedirectToAction("LoginFailed");
        }

        public IActionResult LoginSucceeded()
        {
            return View();
        }

        public IActionResult LoginFailed()
        {
            return View();
        }

        public IActionResult RegisterPage()
        {
            return View();
        }
    }
}
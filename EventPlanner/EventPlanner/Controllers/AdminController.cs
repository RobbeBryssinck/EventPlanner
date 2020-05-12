using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EventPlanner.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private EventPlannerContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        private IWebHostEnvironment _environment;


        public AdminController(EventPlannerContext db, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment)
        {
            this.db = db;
            this.roleManager = roleManager;
            this._environment = environment;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        public IActionResult AdminAccountPage()
        {
            return View(db.Accounts.ToList());
        }

        public IActionResult AdminCoachPage()
        {
            return View(db.Coaches.ToList());
        }

        public IActionResult AdminEventPage()
        {
            return View(db.Events.ToList());
        }

        public IActionResult AdminCategoryPage()
        {
            return View(db.Categories.ToList());
        }
    }
}
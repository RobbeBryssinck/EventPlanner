using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventPlanner.Controllers
{
    public class AccountController : Controller
    {
        private EventPlannerContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EventPlannerContext db)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth
                };
                
                var result = await userManager.CreateAsync(user, model.Password);
                userManager.AddToRoleAsync(user, "User").Wait();

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccountPage()
        {
            var user = await userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> AccountChangePage()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {user.Id} cannot be found";
                return View("Error");
            }

            var model = new AccountChangeViewModel
            {
                id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Password = user.PasswordHash
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AccountChangePage(AccountChangeViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {model.id} cannot be found";
                return View("Error");
            }
            else
            {
                var resultPassword = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
                if (!resultPassword.Succeeded)
                {
                    foreach (var error in resultPassword.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                user.Email = model.Email;
                user.UserName = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.DateOfBirth = model.DateOfBirth;
                var result = await userManager.UpdateAsync(user);

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                if (result.Succeeded && resultPassword.Succeeded)
                {
                    return RedirectToAction("AccountPage");
                }

                return View(model);
            }
        }

        public async Task<IActionResult> EventRegistered()
        {
            EventsViewModel model = new EventsViewModel();
            List<Event> events = new List<Event>();
            var user = await userManager.GetUserAsync(User);
            List<Registration> registrations = db.Registrations.Where(s => s.AccountId == user.Id).ToList();
            
            foreach (var regristation in registrations)
            {
                events.Add(db.Events.Where(s => s.EventId == regristation.EventId).ToList().FirstOrDefault());
            }
            if (events.Count == 0)

            {
                return View("EventsNotFound");
            }
            foreach (var models in events)
            {
                var Participants = db.Registrations.Where(b => b.EventId == models.EventId).Count();
                models.Visitors = Participants;
            }

            model.Events = events;

            return View(model);
        }

        public IActionResult AccountDeletePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AccountDelete(string Id)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {Id} cannot be found";
                return View("Error");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("AccountDeleteComplete", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
                return View("Error");
            }
        }

        public IActionResult AccountDeleteComplete()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
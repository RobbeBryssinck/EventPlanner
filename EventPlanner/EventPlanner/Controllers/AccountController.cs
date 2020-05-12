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

namespace EventPlanner.Controllers
{
    public class AccountController : Controller
    {
        private EventPlannerContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult LoginSucceeded()
        {
            return View();
        }

        public IActionResult LoginFailed()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPage()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPage(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

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
        public IActionResult LoginPage()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoginPage(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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

        public IActionResult AccountPage (int accountID)
        {
            List<Account> accounts = db.Accounts.Where(x => x.AccountId == accountID).ToList();
            Account model = accounts[0];

            return View(model);
        }

        public IActionResult AccountChangePage (int accountID)
        {
            List<Account> accounts = db.Accounts.Where(x => x.AccountId == accountID).ToList();
            Account model = accounts[0];

            return View(model);
        }

        [HttpPost]
        public IActionResult AccountChangePage(Account model)
        {
            List<Account> accounts = db.Accounts.Where(x => x.AccountId == model.AccountId).ToList();
            Account account = accounts[0];

            if (ModelState.IsValid)
            {
                db.Entry(account).CurrentValues.SetValues(model);
                db.SaveChanges();
                return RedirectToAction("AccountPage", new { model.AccountId });
            }

            else
                return View("AccountChangeFailed");
        }

        public IActionResult AccountDeletePage(int accountID)
        {
            List<Account> accounts = db.Accounts.Where(x => x.AccountId == accountID).ToList();
            Account model = accounts[0];

            return View(model);
        }

        public IActionResult AccountDelete(int accountID)
        {
            List<Account> accounts = db.Accounts.Where(x => x.AccountId == accountID).ToList();
            db.Accounts.Remove(accounts[0]);
            db.SaveChanges();
            return RedirectToAction("AccountDeleteComplete");
        }

        public IActionResult AccountDeleteComplete()
        {
            return View();
        }
    }
}
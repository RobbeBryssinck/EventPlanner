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
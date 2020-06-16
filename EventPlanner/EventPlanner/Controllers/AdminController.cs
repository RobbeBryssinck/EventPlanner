using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MimeKit;

namespace EventPlanner.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private EventPlannerContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly string smtpString;
        private readonly string emailFrom;
        private readonly string mailPassword;

        public AdminController(EventPlannerContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
            smtpString = config.GetValue<string>("smtpString");
            emailFrom = config.GetValue<string>("emailFrom");
            mailPassword = config.GetValue<string>("mailPassword");
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                // TODO: implement 404
                return RedirectToAction("Index", "Home");
            }

            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                // TODO: implement 404
                return RedirectToAction("Index", "Home");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRolePost(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                // TODO: implement 404
                return RedirectToAction("Index", "Home");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        public async Task<IActionResult> EditUsersInRole(string roleId, string id)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                // TODO: implement 404
                return RedirectToAction("Index", "Home");
            }

            List<UserRoleViewModel> model = new List<UserRoleViewModel>();

            IQueryable<ApplicationUser> userList = userManager.Users;

            if (!String.IsNullOrEmpty(id))
            {
                userList = userManager.Users.Where(s => s.UserName.Contains(id));
            }
            else
            {
                userList = userManager.Users;
            }

            if (userList.Count() == 0)
            {
                return RedirectToAction("EventsNotFound");
            }

            foreach (var user in userList)
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        public IActionResult AdminAccountPage(string id)
        {
            List<ApplicationUser> Users = new List<ApplicationUser>();

            if (!String.IsNullOrEmpty(id))
            {
                Users = db.Users.Where(s => s.UserName.Contains(id)).ToList();
            }
            else
            {
                Users = db.Users.ToList();
            }

            if (Users.Count == 0)
            {
                return RedirectToAction("EventsNotFound");
            }

            AdminAccountPageViewModel model = new AdminAccountPageViewModel()
            {
                Users = Users
            };

            return View(model);
        }

        public IActionResult AdminCoachPage(string id)
        {
            List<Coach> Coaches = new List<Coach>();

            if (!String.IsNullOrEmpty(id))
            {
                Coaches = db.Coaches.Where(s => s.Name.Contains(id)).ToList();
            }
            else
            {
                Coaches = db.Coaches.ToList();
            }

            if (Coaches.Count == 0)
            {
                return RedirectToAction("EventsNotFound");
            }

            AdminCoachPageViewModel model = new AdminCoachPageViewModel()
            {
                Coaches = Coaches
            };
            return View(model);
        }

        public IActionResult AdminEventPage(string id)
        {
            List<Event> Events = new List<Event>();
            List<Categorie> Categories = new List<Categorie>();

            if (!String.IsNullOrEmpty(id))
            {
                Events = db.Events.Where(s => s.EventName.Contains(id) && s.hidden == false).ToList();
                Categories = db.Categories.Where(s => s.CategorieName.Contains(id) && s.hidden == false).ToList();
            }
            else
            {
                Events = db.Events.Where(s => s.hidden == false).ToList();
                Categories = db.Categories.Where(s => s.hidden == false).ToList();
            }

            if (Events.Count == 0)
            {
                return RedirectToAction("EventsNotFound");
            }

            AdminEventPageViewModel model = new AdminEventPageViewModel()
            {
                Events = Events
            };
            model.Categories = db.Categories.ToList();

            return View(model);
        }

        public IActionResult AdminCategoryPage(string id)
        {
            List<Categorie> Categories = new List<Categorie>();

            if (!String.IsNullOrEmpty(id))
            {
                Categories = db.Categories.Where(s => s.CategorieName.Contains(id) && s.hidden == false).ToList();
            }
            else
            {
                Categories = db.Categories.Where(s => s.hidden == false).ToList();
            }

            if (Categories.Count == 0)
            {
                return RedirectToAction("EventsNotFound");
            }

            AdminCategoryPageViewModel model = new AdminCategoryPageViewModel()
            {
                Categories = Categories
            };

            return View(model);
        }
        public IActionResult EventsNotFound()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public IActionResult AdminAccountDeletePage(string Id)
        {
            AccountDeletePageViewModel model = new AccountDeletePageViewModel
            {
                Id = Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AccountDelete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

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
                    return RedirectToAction("AdminAccountDeleteComplete", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Error");
            }
        }

        public IActionResult AdminAccountDeleteComplete()
        {
            return View();
        }
        public IActionResult AdminNewsletterPage(string id)
        {
            List<Event> Events = new List<Event>();
            List<AdminNewsletterPageViewModel> NewsLetterList = new List<AdminNewsletterPageViewModel>();


            if (!String.IsNullOrEmpty(id))
            {
                Events = db.Events.Where(f => f.EventName.Contains(id) && f.Date > DateTime.Now && f.ForEmployees != EventGroup.RockstarsEmployees && f.hidden == false).ToList();
            }
            else
            {
                Events = db.Events.Where(f => f.Date > DateTime.Now && f.ForEmployees != EventGroup.RockstarsEmployees && f.hidden == false).ToList();
            }

            if (Events.Count == 0)
            {
                return RedirectToAction("EventsNotFound");
            }
            foreach (var item in Events)
            {
                AdminNewsletterPageViewModel newsletterEvent = new AdminNewsletterPageViewModel
                {
                    EventId = item.EventId,
                    EventName = item.EventName
                };
                NewsLetterList.Add(newsletterEvent);
            }

            return View(NewsLetterList);

        }
        [HttpPost]
        public IActionResult NewsLetterPost(List<AdminNewsletterPageViewModel> model)
        {
            List<AdminNewsletterPageViewModel> events = new List<AdminNewsletterPageViewModel>();
            List<MailSubscriber> mailSubscribers = db.MailSubscribers.ToList();
            foreach (var eventmodel in model)
            {
                if (eventmodel.IsSelected)
                {
                    events.Add(eventmodel);
                }
            }


            MimeMessage message = new MimeMessage();
            //from
            MailboxAddress from = new MailboxAddress("Rockstars IT",
            "rockstars.it.project@gmail.com");
            message.From.Add(from);

            //to
            foreach (var mailsubscriber in mailSubscribers)
            {
                MailboxAddress to = new MailboxAddress("User",
                mailsubscriber.Email);
                message.To.Add(to);

                //subject
                message.Subject = "Nieuwsbrief Rockstars IT";

                //body
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Dit zijn de nieuwe evenementen van deze week!</h1>";
                foreach (var item in events)
                {
                    bodyBuilder.HtmlBody += "<a href='https://i406843core.venus.fhict.nl/Event/EventPage?eventID=" + item.EventId + "'>" + item.EventName + "</a><br>";
                }


                message.Body = bodyBuilder.ToMessageBody();

                //connection
                SmtpClient client = new SmtpClient();
                client.Connect(smtpString, 465, true);
                client.Authenticate(emailFrom, mailPassword);

                //send message and dispose
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();

            }

            return RedirectToAction("AdminMailSendSuccess", "Admin");

        }
        public IActionResult AdminMailSendSuccess()
        {
            return View();
        }
    }
}
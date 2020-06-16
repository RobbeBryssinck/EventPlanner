using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventPlanner.Models;
using EventPlanner.Data;
//using System.Net.Mail;
using DnsClient;
using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        private EventPlannerContext db;
        private readonly ILogger<HomeController> _logger;
        private readonly string smtpString;
        private readonly string emailFrom;
        private readonly string mailPassword;

        public HomeController(ILogger<HomeController> logger, EventPlannerContext db, IConfiguration config)
        {
            this.db = db;
            _logger = logger;
            smtpString = config.GetValue<string>("smtpString");
            emailFrom = config.GetValue<string>("emailFrom");
            mailPassword = config.GetValue<string>("mailPassword");
        }

        public IActionResult Index()
        {
            var emp_data = db.Events.Where(f => f.Date > DateTime.Now && f.ForEmployees != EventGroup.RockstarsEmployees && f.hidden == false).OrderBy(e => e.Date).ToList().Take(6);
            HomeIndexViewModel model = new HomeIndexViewModel()
            {
                Events = emp_data
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ErrorUser()
        {
            return View("Error");
        }
        /*public void SendMail()
        {
            MailAddress to = new MailAddress("439702@student.fontys.nl");
            MailAddress from = new MailAddress("notnotheracc@gmail.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Good morning, Elizabeth";
            message.Body = "Elizabeth, Long time no talk. Would you be up for lunch in Soho on Monday? I'm paying.;";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 465)
            {
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required 

            try
            {
                client.Send(message);
                RedirectToAction("Home", "Index");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }*/

        [HttpPost]
        public IActionResult MailSender(string email)
        {
            List<MailSubscriber> mailSubscribers = db.MailSubscribers.Where(x => x.Email == email).ToList();
            
                if (mailSubscribers.Count == 0)
                {
                    MimeMessage message = new MimeMessage();
                    //from
                    MailboxAddress from = new MailboxAddress("Rockstars IT",
                    "rockstars.it.project@gmail.com");
                    message.From.Add(from);

                    //to
                    MailboxAddress to = new MailboxAddress("User",
                    email);
                    message.To.Add(to);

                    //subject
                    message.Subject = "Nieuwsbrief";

                    //body
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = "<h1>U bent nu aangemeld voor de nieuwsbrief!</h1>";
                    bodyBuilder.TextBody = "wat goed!";

                    message.Body = bodyBuilder.ToMessageBody();

                    //connection
                    SmtpClient client = new SmtpClient();
                    client.Connect(smtpString, 465, true);
                    client.Authenticate(emailFrom, mailPassword);

                    //send message and dispose
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();


                    MailSubscriber mailSubscriber = new MailSubscriber()
                    {
                        Email = email
                    };
                    db.MailSubscribers.Add(mailSubscriber);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("EmailSubscribeFail", "Home");
                }
                return RedirectToAction("EmailSubscribeSuccess", "Home");
        }


        public IActionResult EmailSubscribeSuccess()
        {
            return View();
        }
        public IActionResult EmailSubscribeFail()
        {
            return View();
        }
    }
}

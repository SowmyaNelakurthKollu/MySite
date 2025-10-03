using MySite.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        // Store sensitive info in config or environment variables in production!
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "nelakurthisowmya123@gmail.com";
        private const string SenderPassword = "oqulevhmxoutvpuy";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MailModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                using (var mail = new MailMessage())
                {
                    mail.To.Add(model.To);
                    mail.To.Add("sowmyanelakurthi.kollu@proton.me");
                    mail.From = new MailAddress(SenderEmail);
                    mail.Subject = $"Thanks for your Valuable Feedback - {model.Subject}";
                    mail.Body = model.Body;
                    mail.IsBodyHtml = true;

                    using (var smtp = new SmtpClient(SmtpHost, SmtpPort))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                ViewBag.Message = "Mail Sent Successfully!";
                return View("About");
            }
            catch (Exception ex)
            {
                // Log the error as needed
                ViewBag.Message = $"Error sending email: {ex.Message}";
                return View(model);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace MVCTest.Controllers
{
    public class ContactController : Controller
    {

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Confirmed(Contact contact)
        {
            string CtrlName = contact.Name;
            string CtrlEmail = contact.Email;
            string CtrlMessage = contact.Message;

            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            else
            {
                SendEmail(CtrlName, CtrlEmail, CtrlMessage);
                return View();
            }
        }

        public void SendEmail(string Name, string Email, string Message)
        {
     
            /*
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(@"C:\Users\Ice\source\repos\MVCTest\MVCTest")
              .AddJsonFile("appsettings.json")
              .Build();

            string email = configuration.GetSection("EmailSettings").GetSection("email").Value;
            string password = configuration.GetSection("EmailSettings").GetSection("password").Value;
            string smtpserv = configuration.GetSection("EmailSettings").GetSection("smtp").Value;

    */


            MailMessage mail = new MailMessage();
            mail.To.Add("rickv4555@gmail.com");
            mail.From = new MailAddress("rickv4555@gmail.com");
            mail.Subject = "JaxxWaxx Customer" + Email;
            mail.Body = Message;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("rickv4555@gmail.com", "Python2$2");
        }
        
    }
}
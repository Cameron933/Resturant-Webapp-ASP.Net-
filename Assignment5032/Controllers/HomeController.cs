using Assignment5032.Models;
using Assignment5032.Utils;
using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Assignment5032.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Send_EmailAsync(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String entryMails = model.ToEmails;
                    // split target mail box address by ,
                    // store all these to a string array
                    string[] toEmails = entryMails.Split(',');
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    var resp = await es.SendAsync(toEmails, subject, contents);
                    Console.WriteLine(resp);
                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using ExampleApplicationMVC.Filters;
using ExampleApplicationMVC.Models;
using ExampleApplicationMVC.Utilities;
using Microsoft.Win32;
using System.Net.Http;

namespace ExampleApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private IEmailUtil _emailUtil;

        public HomeController(IEmailUtil emailUtil)
        {
            _emailUtil = emailUtil;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Application()
        {
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Application(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return View(applicant);
            }
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.ErrMessage = "Invalid Captcha entry.  Please try again.";
                return View(applicant);
            }
            return View("Upload", applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Application");
            }

            return View(applicant);
        }

        public ActionResult Successful()
        {
            return View();
        }
    }
}
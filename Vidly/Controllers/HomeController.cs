using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Vidly";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About Me";
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Me";
            ViewBag.Message = "Yout contact page.";
            return View();
        }
    }
}

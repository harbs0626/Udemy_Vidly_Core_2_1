using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    public class RentalController : Controller
    {
        public ViewResult Rental()
        {
            ViewBag.Title = "New Rental";

            return View();
        }
    }
}

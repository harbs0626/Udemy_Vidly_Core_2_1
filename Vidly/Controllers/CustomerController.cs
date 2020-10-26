using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private Details_ViewModel _details;
        private ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        public ViewResult Index()
        {
            ViewBag.Title = "Customers";

            this._details = new Details_ViewModel();
            this._details.Customers = this._context.Customers
                .Include(c => c.MembershipType)
                .OrderBy(c => c.Id);

            return View(this._details);
        }
    }
}

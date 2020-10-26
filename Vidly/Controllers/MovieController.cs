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
    public class MovieController : Controller
    {
        private Details_ViewModel _details;
        private ApplicationDbContext _context;
        
        public MovieController(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        public ViewResult Index()
        {
            ViewBag.Title = "Movies";

            this._details = new Details_ViewModel();
            this._details.Movies = this._context.Movies
                .Include(m => m.Genre)
                .OrderBy(m => m.Id);

            return View(this._details);
        }
    }
}

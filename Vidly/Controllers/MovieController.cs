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

        public object NewRecord()
        {
            ViewBag.Title = "New Movie";

            var _viewModel = new Form_Movie_ViewModel()
            {
                Movie = new Movie(),
                Genres = this._context.Genres.ToList()
            };

            return _viewModel;
        }

        public ViewResult Record()
        {
            return View(this.NewRecord());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Details_ViewModel _movie)
        {
            if (ModelState.IsValid)
            {
                if (_movie.Movie.Id == 0)
                {
                    this._context.Movies.Add(_movie.Movie);
                }
                else
                {
                    var _movieInDb = this._context.Movies
                        .SingleOrDefault(c => c.Id == _movie.Movie.Id);

                    if (_movieInDb != null)
                    {
                        _movieInDb.Name = _movie.Movie.Name;
                        _movieInDb.ReleaseDate = _movie.Movie.ReleaseDate;
                        _movieInDb.GenreId = _movie.Movie.GenreId;
                        _movieInDb.NumberInStock = _movie.Movie.NumberInStock;
                    }
                }

                this._context.SaveChanges();
            }
            else
            {
                return View("Form_Movie", this.NewRecord());
            }

            return RedirectToAction("Index", "Movie");
        }

        public ViewResult Details(int Id)
        {
            this._details = new Details_ViewModel();
            this._details.Movies = this._context.Movies
                .Include(m => m.Genre)
                .OrderBy(m => m.Id);

            foreach(var getMovieName in this._details.Movies)
            {
                ViewBag.Title = getMovieName.Name;
            }

            return View(this._details);
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Title = "Edit Movie";

            var _viewModel = new Form_Movie_ViewModel()
            {
                Movie = this._context.Movies.SingleOrDefault(m => m.Id == Id),
                Genres = this._context.Genres.ToList()
            };

            return View("Form_Movie", _viewModel);
        }

        public ActionResult Delete(int Id)
        {
            var _movieInDb = this._context.Movies
                .SingleOrDefault(c => c.Id == Id);

            if (_movieInDb != null)
            {
                this._context.Movies.Remove(_movieInDb);
                this._context.SaveChanges();
            }

            return RedirectToAction("Index", "Movie");
        }

    }
}

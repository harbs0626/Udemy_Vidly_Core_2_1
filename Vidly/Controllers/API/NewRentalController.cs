using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalController : ControllerBase
    {
        public ApplicationDbContext _context;

        public NewRentalController(ApplicationDbContext _ctx)
        {
            this._context = _ctx;
        }

        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var _customers = this._context.Customers
                .Single(c => c.Id == newRental.CustomerId);

            var _movies = this._context.Movies
                .Where(m => newRental.MovieIds.Contains(m.Id));

            foreach (var movie in _movies)
            {
                if (movie.NumberOfAvailability == 0)
                {
                    return BadRequest("Requested movie is not available");
                }

                movie.NumberOfAvailability--;

                var _rentals = new Rental
                {
                    Customer = _customers,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                this._context.Rentals.Add(_rentals);
            }

            return Ok();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private ApplicationDbContext _context;

        public MovieController(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        // GET: API/<controller>
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            return this._context.Movies.ToList()
                .Select(Mapper.Map<Movie, MovieDto>)
                .OrderBy(m => m.Id);
        }

        // GET: API/<controller>/<id>
        [HttpGet("{Id}")]
        public IActionResult GetMovie(int Id)
        {
            var _movie = this._context.Movies
                .SingleOrDefault(m => m.Id == Id);

            if (_movie == null)
            {
                return NotFound("Movie does not exist!");
            }

            return Ok(Mapper.Map<Movie, MovieDto>(_movie));
        }

        // POST: API/<controller>
        [HttpPost]
        public IActionResult CreateMovie(MovieDto _movieDto)
        {
            if (ModelState.IsValid)
            {
                var _movie = Mapper.Map<MovieDto, Movie>(_movieDto);
                this._context.Movies.Add(_movie);
                this._context.SaveChanges();

                _movieDto.Id = _movie.Id;
            }

            return Ok("Successfully added Movie.");
        }

        // PUT: API/<controller>/<id>
        [HttpPut("{Id}")]
        public IActionResult UpdateMovie(int Id, MovieDto _movieDto)
        {
            var _movieInDb = this._context.Movies
                .SingleOrDefault(m => m.Id == Id);

            if (_movieInDb == null)
            {
                return NotFound("Movie does not exist!");
            }
            else
            {
                var _movie = Mapper.Map<MovieDto,Movie>(_movieDto);

                _movieInDb.Name = _movie.Name;
                _movieInDb.ReleaseDate = _movie.ReleaseDate;
                _movieInDb.GenreId = _movie.GenreId;
                _movieInDb.NumberInStock = _movie.NumberInStock;

                this._context.SaveChanges();
            }

            return Ok("Successfully updated Movie.");
        }
        
        // DELETE: API/<controller>/<id>
        [HttpDelete("{Id}")]
        public IActionResult DeleteMovie(int Id)
        {
            var _movieInDb = this._context.Movies
                .SingleOrDefault(m => m.Id == Id);

            if (_movieInDb == null)
            {
                return NotFound("Movie does not exist!");
            }
            else
            {
                this._context.Movies.Remove(_movieInDb);
                this._context.SaveChanges();
            }

            return Ok("Successfully deleted Movie.");
        }

    }
}

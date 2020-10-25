using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models.Interfaces;

namespace Vidly.Models.EntityFrameworks
{
    public class EFGenreRepository : IGenreRepository
    {
        private ApplicationDbContext _context;

        public EFGenreRepository(ApplicationDbContext ctx)
        {
            this._context = ctx;
        }

        public IQueryable<Genre> Genres => this._context.Genres;

        public void SaveGenre(Genre genre)
        {
            if (genre.Id == 0)
            {
                this._context.Genres.Add(genre);
            }
            else
            {
                Genre genreEntry = this._context.Genres
                    .FirstOrDefault(g => g.Id == genre.Id);

                if (genreEntry != null)
                {
                    genreEntry.Name = genre.Name;
                }
            }

            this._context.SaveChanges();
        }
    }
}

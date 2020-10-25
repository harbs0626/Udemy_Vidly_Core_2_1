using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.Interfaces
{
    public interface IGenreRepository
    {
        IQueryable<Genre> Genres { get; }

        void SaveGenre(Genre genre);

    }
}

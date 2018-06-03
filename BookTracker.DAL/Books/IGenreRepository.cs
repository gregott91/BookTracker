using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.DAL.Books
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenresByNameAsync(IEnumerable<string> names);

        Task<IEnumerable<Genre>> SaveGenresAsync(IEnumerable<Genre> genres);
    }
}

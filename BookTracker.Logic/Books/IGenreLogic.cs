using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public interface IGenreLogic
    {
        Task<IEnumerable<Genre>> SaveGenresAsync(IEnumerable<Genre> genres);
    }
}

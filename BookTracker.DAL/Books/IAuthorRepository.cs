using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.DAL.Books
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(IEnumerable<string> names);

        Task<IEnumerable<Author>> SaveAuthorsAsync(IEnumerable<Author> authors);
    }
}

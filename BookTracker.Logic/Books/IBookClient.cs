using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public interface IBookClient
    {
        Task<Book> GetBookByIsbn(string isbn);
    }
}

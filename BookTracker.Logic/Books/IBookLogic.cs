using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public interface IBookLogic
    {
        Task<Book> GetBookByIsbnAsync(string isbn);

        Task<Book> SaveBookAsync(Book book);
    }
}

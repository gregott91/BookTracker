using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.DAL.Books
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIsbnAsync(string isbn);

        Task<Book> SaveBookAsync(Book book);
    }
}

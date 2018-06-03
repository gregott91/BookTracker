using BookTracker.DAL.Books;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public class BookLogic : IBookLogic
    {
        private IBookRepository bookRepo;

        public BookLogic(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        public async Task<Book> GetBookByIsbnAsync(string isbn)
        {
            return await this.bookRepo.GetBookByIsbnAsync(isbn);
        }

        public async Task<Book> SaveBookAsync(Book book)
        {
            return await this.bookRepo.SaveBookAsync(book);
        }
    }
}

using BookTracker.Models.ApiClient;
using BookTracker.Models.Book;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public interface IBooksClient
    {
        Task<IEnumerable<BookSearchResult>> SearchBooksAsync(string searchQuery);
    }
}

using BookTracker.Models.Book;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public interface IBookPropertiesLogic
    {
        Task UpdateBookProperties(string userId, BookSearchResult book, bool isCurrentlyReading, bool isBookToRead, bool isInBookshelf, bool isInterested);
    }
}

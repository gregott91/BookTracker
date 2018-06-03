using BookTracker.DAL.Books;
using BookTracker.Logic.Image;
using BookTracker.Models.Book;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public class BookPropertiesLogic : IBookPropertiesLogic
    {
        private IBookLogic bookLogic;
        private IAuthorLogic authorLogic;
        private IGenreLogic genreLogic;
        private IImageLogic imageLogic;

        public BookPropertiesLogic(IBookLogic bookLogic,
            IAuthorLogic authorLogic,
            IGenreLogic genreLogic,
            IImageLogic imageLogic)
        {
            this.bookLogic = bookLogic;
            this.authorLogic = authorLogic;
            this.genreLogic = genreLogic;
            this.imageLogic = imageLogic;
        }

        public async Task UpdateBookProperties(string userId, BookSearchResult bookSearchResult, bool isCurrentlyReading, bool isBookToRead, bool isInBookshelf, bool isInterested)
        {
            var book = new Book();
            book.Title = bookSearchResult.Title;
            book.Description = bookSearchResult.Description;
            book.Rating = bookSearchResult.Rating;
            book.RatingCount = bookSearchResult.RatingCount;
            book.PublishedDate = bookSearchResult.PublishedDate;
            book.PageCount = bookSearchResult.PageCount;
            book.Isbn = bookSearchResult.Isbn;

            if (bookSearchResult.Authors != null)
            {
                book.Authors = bookSearchResult.Authors.Select(x => new Author(x));
            }

            if (bookSearchResult.Genres != null)
            {
                book.Genres = bookSearchResult.Genres.Select(x => new Genre(x));
            }

            var savedAuthors = await this.authorLogic.SaveAuthorsAsync(book.Authors);
            var savedGenres = await this.genreLogic.SaveGenresAsync(book.Genres);
            var savedBook = await this.bookLogic.GetBookByIsbnAsync(book.Isbn);

            if (savedBook == null)
            {
                book.CoverImage = await this.imageLogic.GetImageFromUrlAsync(bookSearchResult.CoverImageUrl);
                savedBook = await this.bookLogic.SaveBookAsync(book);
            }


        }
    }
}

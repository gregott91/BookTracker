using BookTracker.Logic;
using BookTracker.Logic.ApiClient;
using BookTracker.Logic.Books;
using BookTracker.Models.ApiClient;
using BookTracker.Models.Book;
using BookTracker.Models.Books;
using BookTracker.Models.BookViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookTracker.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private ILogger logger;

        public BookController(ILogger<BookController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Bookshelf()
        {
            return View();
        }

        public IActionResult FindBooks()
        {
            return View();
        }

        public async Task<JsonResult> SearchBooksAsync(string searchQuery, [FromServices] IBooksClient booksClient)
        {
            var results = await booksClient.SearchBooksAsync(searchQuery);

            List<BookSearchResultViewModel> models = new List<BookSearchResultViewModel>();

            foreach (var result in results)
            {
                BookSearchResultViewModel model = new BookSearchResultViewModel();

                model.Title = result.Title;
                model.Description = result.Description;
                model.CoverImageUrl = result.CoverImageUrl;
                model.Rating = result.Rating == null ? null : result.Rating.ToString();
                model.RatingCount = result.RatingCount.ToString();
                model.PublishedDate = result.PublishedDate == null ? null : result.PublishedDate.ToString();
                model.PageCount = result.PageCount == null ? null : result.PageCount.ToString();
                model.Isbn = result.Isbn;

                if (result.Authors != null)
                {
                    model.Authors = string.Join(", ", result.Authors);
                }

                if (result.Genres != null)
                {
                    model.Genres = string.Join(", ", result.Genres);
                }

                if (result.PublishedDate != null)
                {
                    model.Year = result.PublishedDate.Value.Year.ToString();
                }

                models.Add(model);
            }

            return Json(models);
        }

        public async Task<JsonResult> GetUserBookPropertiesAsync(string isbn, [FromServices] IUserBooksLogic booksLogic)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var properties = await booksLogic.GetUserBookPropertiesAsync(userId, isbn);
            var propertiesViewModel = new UserBookPropertiesViewModel();

            if (properties != null)
            {
                var bookHistory = new List<BookHistoryViewModel>();
                if (properties.UserBookHistory != null)
                {
                    foreach (var historyItem in properties.UserBookHistory)
                    {
                        bookHistory.Add(new BookHistoryViewModel()
                        {
                            StartDate = historyItem.StartDate,
                            EndDate = historyItem.EndDate
                        });
                    }
                }

                propertiesViewModel.BookHistory = bookHistory;

                propertiesViewModel.IsBookToRead = properties.UserBookToRead != null;
                propertiesViewModel.IsCurrentlyReading = properties.UserBookReading != null;
                propertiesViewModel.IsInBookshelf = properties.UserBookshelf != null && properties.UserBookshelf.IsInterested == false;
                propertiesViewModel.IsInterested = properties?.UserBookshelf?.IsInterested ?? false;
            };

            return Json(propertiesViewModel);
        }

        public async Task UpdateBookPropertiesAsync(BookSearchResultViewModel book, UserBookPropertiesViewModel properties, [FromServices] IBookPropertiesLogic bookLogic)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var bookToSave = new BookSearchResult();
                bookToSave.Title = book.Title;
                bookToSave.Description = book.Description;
                bookToSave.CoverImageUrl = book.CoverImageUrl;
                bookToSave.Rating = ParseExtensions.TryParseDouble(book.Rating);
                bookToSave.RatingCount = ParseExtensions.TryParseInt(book.RatingCount, 0).Value;
                bookToSave.PublishedDate = ParseExtensions.TryParseDateTime(book.PublishedDate);
                bookToSave.PageCount = ParseExtensions.TryParseInt(book.PageCount);
                bookToSave.Isbn = book.Isbn;

                if (book.Authors != null)
                {
                    bookToSave.Authors = book.Authors.Split(",");
                }

                if (book.Genres != null)
                {
                    bookToSave.Genres = book.Genres.Split(",");
                }

                await bookLogic.UpdateBookProperties(userId, bookToSave, properties.IsCurrentlyReading, properties.IsBookToRead, properties.IsInBookshelf, properties.IsInterested);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to update book properties.");

                Response.StatusCode = 500;
            }
        }
    }
}

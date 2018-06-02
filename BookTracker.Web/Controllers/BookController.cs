using BookTracker.Logic.ApiClient;
using BookTracker.Logic.Books;
using BookTracker.Models.ApiClient;
using BookTracker.Models.BookViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var results = await booksClient.SearchBooks(searchQuery);

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

        public async Task<JsonResult> GetUserBookProperties(string isbn, [FromServices] IUserBooksLogic booksLogic)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await booksLogic.GetUserBookProperties(userId, isbn);

            return Json(new UserBookPropertiesViewModel());
        }
    }
}

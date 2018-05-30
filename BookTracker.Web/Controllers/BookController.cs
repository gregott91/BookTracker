using BookTracker.Logic.ApiClient;
using BookTracker.Models.ApiClient;
using BookTracker.Models.BookViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private IBooksClient booksClient;

        public BookController(IBooksClient booksClient)
        {
            this.booksClient = booksClient;
        }

        public IActionResult Bookshelf()
        {
            return View();
        }

        public IActionResult FindBooks()
        {
            return View();
        }

        public async Task<JsonResult> SearchBooksAsync(string searchQuery)
        {
            BookSearchResult results = await booksClient.SearchBooks(searchQuery);

            List<BookSearchResultViewModel> models = new List<BookSearchResultViewModel>();

            foreach (var result in results.Items)
            {
                BookSearchResultViewModel model = new BookSearchResultViewModel();

                model.Title = result.VolumeInfo?.Title;
                model.Description = result.VolumeInfo?.Description;
                model.CoverImageUrl = result.VolumeInfo?.ImageLinks.Thumbnail;
                model.Rating = result.VolumeInfo?.AverageRating;
                model.RatingCount = result.VolumeInfo?.RatingsCount;
                model.PublishedDate = result.VolumeInfo?.PublishedDate;

                if (result.VolumeInfo?.Authors != null)
                {
                    model.Authors = string.Join(", ", result.VolumeInfo?.Authors);
                }

                if (result.VolumeInfo?.Categories != null)
                {
                    model.Categories = string.Join(", ", result.VolumeInfo?.Categories);
                }

                DateTime bookDate;
                if (DateTime.TryParse(result.VolumeInfo?.PublishedDate, out bookDate))
                {
                    model.Year = bookDate.Year.ToString();
                }
                else if (!string.IsNullOrEmpty(result.VolumeInfo?.PublishedDate))
                {
                    model.Year = result.VolumeInfo?.PublishedDate;
                }

                models.Add(model);
            }

            return Json(models);
        }
    }
}

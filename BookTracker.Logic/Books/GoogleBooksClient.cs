using BookTracker.Logic.ApiClient;
using BookTracker.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public class GoogleBooksClient : IBooksClient
    {
        private IGoogleBooksApiClient apiClient;

        public GoogleBooksClient(IGoogleBooksApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IEnumerable<BookSearchResult>> SearchBooks(string searchQuery)
        {
            var apiResults = await apiClient.SearchBooks(searchQuery);

            var results = new List<BookSearchResult>();

            foreach (var apiResult in apiResults.Items)
            {
                if (apiResult.VolumeInfo == null)
                {
                    continue;
                }

                var result = new BookSearchResult();

                result.Title = apiResult.VolumeInfo.Title;
                result.Description = apiResult.VolumeInfo.Description;
                result.CoverImageUrl = apiResult.VolumeInfo.ImageLinks.Thumbnail;
                result.Rating = ParseExtensions.TryParseDouble(apiResult.VolumeInfo.AverageRating);
                result.RatingCount = ParseExtensions.TryParseInt(apiResult.VolumeInfo.RatingsCount, 0).Value;
                result.PublishedDate = ParseExtensions.TryParseDateTime(apiResult.VolumeInfo.PublishedDate, null);
                result.PageCount = ParseExtensions.TryParseInt(apiResult.VolumeInfo.PageCount);
                result.Authors = apiResult.VolumeInfo.Authors;
                result.Genres = apiResult.VolumeInfo.Categories;
                result.PublishedDate = ParseExtensions.TryParseDateTime(apiResult.VolumeInfo.PublishedDate);

                // sometimes the API returns only the year
                if (result.PublishedDate == null && apiResult.VolumeInfo.PublishedDate.Length == 4)
                {
                    int year;
                    if (int.TryParse(apiResult.VolumeInfo.PublishedDate, out year))
                    {
                        result.PublishedDate = new DateTime(year, 1, 1);
                    }
                }

                if (apiResult.VolumeInfo.IndustryIdentifiers != null)
                {
                    var isbn = apiResult.VolumeInfo.IndustryIdentifiers.Where(x => x.Type == "ISBN_13").FirstOrDefault();

                    if (isbn == null)
                    {
                        isbn = apiResult.VolumeInfo.IndustryIdentifiers.Where(x => x.Type == "ISBN_10").FirstOrDefault();
                    }

                    if (isbn == null)
                    {
                        isbn = apiResult.VolumeInfo.IndustryIdentifiers.Where(x => x.Type == "ISBN").FirstOrDefault();
                    }

                    if (isbn != null)
                    {
                        result.Isbn = isbn.Identifier;
                    }
                }

                results.Add(result);
            }

            return results;
        }
    }
}

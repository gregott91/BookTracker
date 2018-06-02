using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Book
{
    public class BookSearchResult
    {
        public string Title { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string Description { get; set; }

        public string CoverImageUrl { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public double? Rating { get; set; }

        public int RatingCount { get; set; }

        public int? PageCount { get; set; }

        public string Isbn { get; set; }
    }
}

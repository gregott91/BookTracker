using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string Description { get; set; }

        public byte[] CoverImage { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public double? Rating { get; set; }

        public int RatingCount { get; set; }

        public int? PageCount { get; set; }

        public string Isbn { get; set; }
    }
}

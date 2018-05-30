using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Models.BookViewModels
{
    public class BookSearchResultViewModel
    {
        public string Title { get; set; }

        public string Authors { get; set; }

        public string PublishedDate { get; set; }

        public string Year { get; set; }

        public string Description { get; set; }

        public string CoverImageUrl { get; set; }

        public bool IsExpanded { get; set; }

        public string Categories { get; set; }

        public string Rating { get; set; }

        public string RatingCount { get; set; }
    }
}

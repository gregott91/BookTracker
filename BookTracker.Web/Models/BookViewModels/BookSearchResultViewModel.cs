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

        public string Year { get; set; }

        public string Description { get; set; }

        public string CoverImageUrl { get; set; }
    }
}

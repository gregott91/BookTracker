using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.ApiClient
{
    public class BookSearchResult
    {
        public IEnumerable<BookSearchResultItem> Items { get; set; }
    }
}

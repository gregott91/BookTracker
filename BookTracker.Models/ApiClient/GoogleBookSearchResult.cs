using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.ApiClient
{
    public class GoogleBookSearchResult
    {
        public IEnumerable<GoogleBookSearchResultItem> Items { get; set; }
    }
}

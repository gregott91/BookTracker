using BookTracker.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.ApiClient
{
    public interface IBooksClient
    {
        Task<BookSearchResult> SearchBooks(string searchQuery);
    }
}

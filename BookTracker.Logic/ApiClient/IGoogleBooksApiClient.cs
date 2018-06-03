using BookTracker.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.ApiClient
{
    public interface IGoogleBooksApiClient
    {
        Task<GoogleBookSearchResult> SearchBooksAsync(string searchQuery);
    }
}

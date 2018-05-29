using BookTracker.Models.ApiClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.ApiClient
{
    public class GoogleBooksClient : IBooksClient
    {
        private HttpClient _client;
        private ILogger<GoogleBooksClient> _logger;
        private readonly GoogleApiKey _apiKey;

        public GoogleBooksClient(ILogger<GoogleBooksClient> logger, GoogleApiKey apiKey)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"https://www.googleapis.com");
            _logger = logger;
            _apiKey = apiKey;
        }

        public async Task<BookSearchResult> SearchBooks(string searchQuery)
        {
            var queryUrl = new Uri($"/books/v1/volumes?q={searchQuery}", UriKind.Relative);
            var res = await _client.GetAsync(queryUrl);
            return await res.Content.ReadAsAsync<BookSearchResult>();
        }
    }
}
using BookTracker.Models.ApiClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.ApiClient
{
    public class GoogleBooksApiClient : IGoogleBooksApiClient
    {
        private HttpClient _client;
        private ILogger<GoogleBooksApiClient> _logger;
        private readonly GoogleApiKey _apiKey;

        public GoogleBooksApiClient(ILogger<GoogleBooksApiClient> logger, GoogleApiKey apiKey)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"https://www.googleapis.com");
            _logger = logger;
            _apiKey = apiKey;
        }

        public async Task<GoogleBookSearchResult> SearchBooksAsync(string searchQuery)
        {
            var queryUrl = new Uri($"/books/v1/volumes?q={searchQuery}", UriKind.Relative);
            var res = await _client.GetAsync(queryUrl);
            return await res.Content.ReadAsAsync<GoogleBookSearchResult>();
        }
    }
}
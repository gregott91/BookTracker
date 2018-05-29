using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.ApiClient
{
    public class GoogleApiKey
    {
        public string ApiKey { get; private set; }

        public GoogleApiKey(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}

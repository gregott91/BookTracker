using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.ApiClient
{
    public class GoogleBookSearchResultItem
    {
        public string Id { get; set; }

        public VolumeInfo VolumeInfo { get; set; }
    }
}

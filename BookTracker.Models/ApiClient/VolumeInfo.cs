﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.ApiClient
{
    public class VolumeInfo
    {
        public string Title { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public string PublishedDate { get; set; }

        public string Description { get; set; }

        public ImageLinks ImageLinks { get; set; }
    }
}
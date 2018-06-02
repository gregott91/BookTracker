using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Models.BookViewModels
{
    public class UserBookPropertiesViewModel
    {
        public bool IsInterested { get; set; }

        public bool IsInBookshelf { get; set; }

        public bool IsBookToRead { get; set; }

        public bool IsCurrentlyReading { get; set; }

        public IEnumerable<BookHistoryViewModel> BookHistory { get; set; }
    }
}

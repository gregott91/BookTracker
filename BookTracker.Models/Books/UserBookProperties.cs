using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class UserBookProperties
    {
        public UserBookToRead UserBookToRead { get; set; }

        public UserBookshelf UserBookshelf { get; set; }

        public UserBookReading UserBookReading { get; set; }

        public IEnumerable<UserBookHistory> UserBookHistory { get; set; }
    }
}

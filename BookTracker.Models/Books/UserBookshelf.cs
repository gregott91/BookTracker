using BookTracker.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class UserBookshelf
    {
        public int UserBookshelfId { get; set; }

        public Book Book { get; set; }

        public AspNetUser User { get; set; }

        public bool IsInterested { get; set; }
    }
}

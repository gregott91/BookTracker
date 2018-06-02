using BookTracker.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class UserBookToRead
    {
        public int UserBookToReadId { get; set; }

        public Book Book { get; set; }

        public AspNetUser User { get; set; }

        public int OrderId { get; set; }
    }
}

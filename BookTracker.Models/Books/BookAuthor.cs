using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}

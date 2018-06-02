using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class BookGenre
    {
        public int BookGenreId { get; set; }

        public Book Book { get; set; }

        public Genre Genre { get; set; }
    }
}

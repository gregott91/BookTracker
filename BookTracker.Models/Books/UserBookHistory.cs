using BookTracker.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Books
{
    public class UserBookHistory
    {
        public int UserBookHistoryId { get; set; }

        public Book Book { get; set; }

        public AspNetUser User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

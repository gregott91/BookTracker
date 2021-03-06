﻿using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.DAL.Books
{
    public interface IUserBooksRepository
    {
        Task<UserBookProperties> GetUserBookPropertiesAsync(string userId, string bookIsbn);
    }
}

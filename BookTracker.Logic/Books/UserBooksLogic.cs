using BookTracker.DAL.Books;
using BookTracker.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.Logic.Books
{
    public class UserBooksLogic : IUserBooksLogic
    {
        private IUserBooksRepository booksRepository;

        public UserBooksLogic(IUserBooksRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public async Task<UserBookProperties> GetUserBookProperties(string userId, string isbn)
        {
            return await booksRepository.GetUserBookProperties(userId, isbn);
        }
    }
}

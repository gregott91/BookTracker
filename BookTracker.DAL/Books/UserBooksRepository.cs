using BookTracker.Models.Books;
using BookTracker.Models.Connection;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.DAL.Books
{
    public class UserBooksRepository : IUserBooksRepository
    {
        private SqliteConnectionString connectionString;

        public UserBooksRepository(SqliteConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<UserBookProperties> GetUserBookPropertiesAsync(string userId, string bookIsbn)
        {
            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                var book = await dbConnection.QuerySingleOrDefaultAsync<Book>($"SELECT * FROM Book WHERE Isbn = '@Isbn'", new { Isbn = bookIsbn });

                if (book == null)
                {
                    return null;
                }

                string sql = "SELECT * FROM UserBookToRead WHERE BookId = @BookId AND UserId = '@UserId'; " +
                    "SELECT * FROM UserBookshelf WHERE BookId = @BookId AND UserId = '@UserId';" +
                    "SELECT * FROM UserBookHistory WHERE BookId = @BookId AND UserId = '@UserId';" +
                    "SELECT * FROM UserBookReading WHERE BookId = @BookId AND UserId = '@UserId';";

                using (var multi = dbConnection.QueryMultiple(sql, new { BookId = book.BookId, UserId = userId }))
                {
                    var bookToRead = await multi.ReadFirstOrDefaultAsync<UserBookToRead>();
                    var userBookshelf = await multi.ReadFirstOrDefaultAsync<UserBookshelf>();
                    var userBookHistory = await multi.ReadAsync<UserBookHistory>();
                    var userBookReading = await multi.ReadFirstOrDefaultAsync<UserBookReading>();

                    return new UserBookProperties()
                    {
                        UserBookHistory = userBookHistory,
                        UserBookToRead = bookToRead,
                        UserBookReading = userBookReading,
                        UserBookshelf = userBookshelf
                    };
                }
            }
        }
    }
}

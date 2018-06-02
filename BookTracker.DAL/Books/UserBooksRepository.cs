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

        public async Task<UserBookProperties> GetUserBookProperties(string userId, string bookIsbn)
        {
            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                var book = await dbConnection.QueryAsync<Book>($"SELECT * FROM Book WHERE Isbn = '{bookIsbn}'");

                if (book == null)
                {
                    return null;
                }

                return null;
            }
        }
    }
}

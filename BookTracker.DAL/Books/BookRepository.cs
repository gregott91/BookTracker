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
    public class BookRepository : IBookRepository
    {
        private SqliteConnectionString connectionString;

        public BookRepository(SqliteConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Book> GetBookByIsbnAsync(string isbn)
        {
            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                return await dbConnection.QuerySingleOrDefaultAsync<Book>($"SELECT * FROM Book WHERE Isbn = '@Isbn'", new { Isbn = isbn });
            }
        }

        public async Task<Book> SaveBookAsync(Book book)
        {
            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                var insertCommand = "INSERT INTO Book" +
                                    "(Title, PublishedDate, Description, CoverImage, Rating, RatingCount, PageCount, Isbn)" +
                                    "VALUES" +
                                    "(@Title, @PublishedDate, @Description, @CoverImage, @Rating, @RatingCount, @PageCount, @Isbn);" +
                                    "SELECT last_insert_rowid();";

                var id = await dbConnection.QueryFirstAsync<int>(insertCommand, new {
                    Title = book.Title,
                    PublishedDate = book.PublishedDate,
                    Description = book.Description,
                    CoverImage = book.CoverImage,
                    Rating = book.Rating,
                    RatingCount = book.RatingCount,
                    PageCount = book.PageCount,
                    Isbn = book.Isbn
                });

                book.BookId = id;

                return book;
            }
        }
    }
}

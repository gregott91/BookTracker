using BookTracker.Models.Books;
using BookTracker.Models.Connection;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTracker.DAL.Books
{
    public class AuthorRepository : IAuthorRepository
    {
        private SqliteConnectionString connectionString;

        public AuthorRepository(SqliteConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(IEnumerable<string> names)
        {
            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                var query = "SELECT * FROM Author WHERE Name IN @Names";
                return await dbConnection.QueryAsync<Author>(query, new { Names = names });
            }
        }

        public async Task<IEnumerable<Author>> SaveAuthorsAsync(IEnumerable<Author> authors)
        {
            if (authors == null || authors.Count() == 0)
            {
                return authors;
            }

            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                DynamicParameters parameters = new DynamicParameters();
                StringBuilder insertCommandBuilder = new StringBuilder("INSERT INTO Author (Name)");

                int index = 0;
                foreach (var author in authors)
                {
                    parameters.Add($"Name_{index}", author.Name);
                    insertCommandBuilder.Append($"VALUES ('@Name_{index}')");

                    index++;
                }

                await dbConnection.ExecuteAsync(insertCommandBuilder.ToString(), parameters);

                return await GetAuthorsByNameAsync(authors.Select(x => x.Name));
            }
        }
    }
}

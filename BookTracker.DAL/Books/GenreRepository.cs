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
    public class GenreRepository : IGenreRepository
    {
        private SqliteConnectionString connectionString;

        public GenreRepository(SqliteConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Genre>> GetGenresByNameAsync(IEnumerable<string> names)
        {
            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                var query = "SELECT * FROM Genre WHERE Name IN @Names";
                return await dbConnection.QueryAsync<Genre>(query, new { Names = names });
            }
        }

        public async Task<IEnumerable<Genre>> SaveGenresAsync(IEnumerable<Genre> genres)
        {
            if (genres == null || genres.Count() == 0)
            {
                return genres;
            }

            using (var dbConnection = new SqliteConnection(connectionString.ConnectionString))
            {
                dbConnection.Open();

                DynamicParameters parameters = new DynamicParameters();
                StringBuilder insertCommandBuilder = new StringBuilder("INSERT INTO Genre (Name)");

                int index = 0;
                foreach (var genre in genres)
                {
                    parameters.Add($"Name_{index}", genre.Name);
                    insertCommandBuilder.Append($"VALUES ('@Name_{index}')");

                    index++;
                }

                await dbConnection.ExecuteAsync(insertCommandBuilder.ToString(), parameters);

                return await GetGenresByNameAsync(genres.Select(x => x.Name));
            }
        }
    }
}

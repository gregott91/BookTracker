using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Models.Connection
{
    public class SqliteConnectionString
    {
        public string ConnectionString { get; private set; }

        public SqliteConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}

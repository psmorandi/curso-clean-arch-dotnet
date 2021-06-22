using Npgsql;
using System.Data;

namespace CleanArch.School.Application.Infra.Database
{
    public class PostgresConnectionPool
    {
        private readonly string connectionString;

        public PostgresConnectionPool(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection => new NpgsqlConnection(this.connectionString);
    }
}
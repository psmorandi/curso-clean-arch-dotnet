namespace CleanArch.School.Application.Infra.Database
{
    using System.Data;
    using Npgsql;

    public class PostgresConnectionPool
    {
        private readonly string connectionString;

        public PostgresConnectionPool(string connectionString) => this.connectionString = connectionString;

        public IDbConnection CreateConnection() => new NpgsqlConnection(this.connectionString);
    }
}
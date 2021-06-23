namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using Infra.Database;

    public abstract class BaseRepositoryDatabase
    {
        protected readonly PostgresConnectionPool ConnectionPool;

        protected BaseRepositoryDatabase(PostgresConnectionPool connectionPool) => this.ConnectionPool = connectionPool;
    }
}
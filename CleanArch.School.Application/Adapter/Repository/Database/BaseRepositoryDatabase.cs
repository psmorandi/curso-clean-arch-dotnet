namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using Infra.Database;

    public abstract class BaseRepositoryDatabase
    {
        protected readonly PostgresConnectionPool connectionPool;

        protected BaseRepositoryDatabase(PostgresConnectionPool connectionPool) => this.connectionPool = connectionPool;
    }
}
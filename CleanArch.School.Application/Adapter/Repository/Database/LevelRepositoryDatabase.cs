namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Entity;
    using Domain.Repository;
    using Infra.Database;

    public class LevelRepositoryDatabase : BaseRepositoryDatabase, ILevelRepository
    {
        public LevelRepositoryDatabase(PostgresConnectionPool connectionPool)
            : base(connectionPool) { }

        public async Task Save(Level level)
        {
            using var connection = this.ConnectionPool.CreateConnection();
            await connection.ExecuteAsync("insert into system.level (code, description) values (@Code, @Description)", new { level.Code, level.Description });
        }

        public async Task<Level> FindByCode(string code)
        {
            using var connection = this.ConnectionPool.CreateConnection();
            return await connection.QuerySingleAsync<Level>("select code as Code, description as Description from system.level where code = @code", new { code });
        }
    }
}
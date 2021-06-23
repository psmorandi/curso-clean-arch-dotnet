namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Entity;
    using Domain.Repository;
    using Infra.Database;

    public class ModuleRepositoryDatabase : BaseRepositoryDatabase, IModuleRepository
    {
        public ModuleRepositoryDatabase(PostgresConnectionPool connectionPool)
            : base(connectionPool) { }

        public async Task Save(Module module)
        {
            using var connection = this.ConnectionPool.CreateConnection();
            var parameters = new { module.Level, module.Code, module.Description, module.MinimumAge, module.Price };
            await connection.ExecuteAsync(
                "insert into system.module (level, code, description, minimum_age, price) values (@Level, @Code, @Description, @MinimumAge, @Price",
                parameters);
        }

        public async Task<Module> FindByCode(string level, string module)
        {
            using var connection = this.ConnectionPool.CreateConnection();
            return await connection.QuerySingleAsync<Module>(
                       "select level as Level, code as Code, description as Description, minimum_age as MinimumAge, price as Price from system.module where level = @level and module = @module",
                       new { level, module });
        }
    }
}
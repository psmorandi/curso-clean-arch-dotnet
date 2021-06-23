namespace CleanArch.School.Application.Adapter.Repository.Database
{
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Entity;
    using Domain.Repository;
    using Extensions;
    using Infra.Database;

    public class ClassroomRepositoryDatabase : BaseRepositoryDatabase, IClassroomRepository
    {
        public ClassroomRepositoryDatabase(PostgresConnectionPool connectionPool)
            : base(connectionPool) { }

        public async Task Save(Classroom classroom)
        {
            using var connection = this.ConnectionPool.CreateConnection();
            await connection.ExecuteAsync(
                "insert into system.classroom (level, module, code, capacity, start_date, end_date) values (@Level, @Module, @Code, @Capacity, @StartDate, @EndDate)",
                new
                {
                    classroom.Level,
                    classroom.Module,
                    classroom.Code,
                    classroom.Capacity,
                    StartDate = classroom.Period.StartDate.ToDateTime(),
                    EndDate = classroom.Period.EndDate.ToDateTime()
                });
        }

        public async Task<Classroom> FindByCode(string level, string module, string classroom)
        {
            using var connection = this.ConnectionPool.CreateConnection();
            return await connection.QuerySingleAsync<Classroom>(
                       "select level, module, code, capacity, start_date, end_date where level = @level and module = @module and code = @classroom",
                       new { level, module, classroom });
        }
    }
}
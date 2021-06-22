namespace CleanArch.School.Application.Adapter.Factory
{
    using CleanArch.School.Application.Adapter.Repository.Database;
    using CleanArch.School.Application.Infra.Database;
    using Domain.Factory;
    using Domain.Repository;

    public class RepositoryDatabaseAbstractFactory : IRepositoryAbstractFactory
    {
        private readonly LevelRepositoryDatabase levelRepository;
        public RepositoryDatabaseAbstractFactory(PostgresConnectionPool connectionPool)
        {
            this.levelRepository = new LevelRepositoryDatabase(connectionPool);
        }

        public ILevelRepository CreateLevelRepository() => this.levelRepository;

        public IModuleRepository CreateModuleRepository() => throw new System.NotImplementedException();

        public IClassroomRepository CreateClassroomRepository() => throw new System.NotImplementedException();

        public IEnrollmentRepository CreateEnrollmentRepository() => throw new System.NotImplementedException();
    }
}
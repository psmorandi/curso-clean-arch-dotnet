namespace CleanArch.School.Application.Adapter.Factory
{
    using System;
    using Domain.Factory;
    using Domain.Repository;
    using Infra.Database;
    using Repository.Database;

    public class RepositoryDatabaseAbstractFactory : IRepositoryAbstractFactory
    {
        private readonly PostgresConnectionPool connectionPool;

        public RepositoryDatabaseAbstractFactory(PostgresConnectionPool connectionPool) => this.connectionPool = connectionPool;

        public ILevelRepository CreateLevelRepository() => new LevelRepositoryDatabase(this.connectionPool);

        public IModuleRepository CreateModuleRepository() => new ModuleRepositoryDatabase(this.connectionPool);

        public IClassroomRepository CreateClassroomRepository() => new ClassroomRepositoryDatabase(this.connectionPool);

        public IEnrollmentRepository CreateEnrollmentRepository() => throw new NotImplementedException();
    }
}
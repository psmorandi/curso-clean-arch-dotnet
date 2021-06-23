namespace CleanArch.School.Application.Adapter.Factory
{
    using System;
    using Domain.Factory;
    using Domain.Repository;
    using Infra.Database;
    using Repository.Database;

    public class RepositoryDatabaseAbstractFactory : IRepositoryAbstractFactory
    {
        private readonly LevelRepositoryDatabase levelRepository;
        private readonly ModuleRepositoryDatabase moduleRepository;

        public RepositoryDatabaseAbstractFactory(PostgresConnectionPool connectionPool)
        {
            this.levelRepository = new LevelRepositoryDatabase(connectionPool);
            this.moduleRepository = new ModuleRepositoryDatabase(connectionPool);
        }

        public ILevelRepository CreateLevelRepository() => this.levelRepository;

        public IModuleRepository CreateModuleRepository() => this.moduleRepository;

        public IClassroomRepository CreateClassroomRepository() => throw new NotImplementedException();

        public IEnrollmentRepository CreateEnrollmentRepository() => throw new NotImplementedException();
    }
}
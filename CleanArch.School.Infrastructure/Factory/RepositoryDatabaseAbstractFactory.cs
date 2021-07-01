namespace CleanArch.School.Infrastructure.Factory
{
    using Application.Factory;
    using Application.Repository;
    using AutoMapper;
    using Database;
    using Repository.Database;

    public class RepositoryDatabaseAbstractFactory : IRepositoryAbstractFactory
    {
        private readonly SchoolDbContext dbContext;
        private readonly IMapper mapper;

        public RepositoryDatabaseAbstractFactory(SchoolDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ILevelRepository CreateLevelRepository() => new LevelRepositoryDatabase(this.dbContext, this.mapper);

        public IModuleRepository CreateModuleRepository() => new ModuleRepositoryDatabase(this.dbContext, this.mapper);

        public IClassroomRepository CreateClassroomRepository() => new ClassroomRepositoryDatabase(this.dbContext, this.mapper);

        public IEnrollmentRepository CreateEnrollmentRepository() => new EnrollmentRepositoryDatabase(this.dbContext, this.mapper);
    }
}
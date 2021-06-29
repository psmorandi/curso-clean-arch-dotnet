namespace CleanArch.School.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Application.Adapter.Factory;
    using Application.Domain.Factory;
    using Application.Domain.Repository;
    using Application.Domain.UseCase;
    using Application.Infra.Database;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    // ReSharper disable InconsistentNaming
    public class BaseDatabaseTests : IDisposable
    {
        protected readonly SchoolDbContext dbContext;
        protected readonly EnrollStudent enrollStudent;
        protected readonly GetEnrollment getEnrollment;
        protected readonly IRepositoryAbstractFactory repositoryFactory;
        protected readonly ILevelRepository levelRepository;
        protected readonly IModuleRepository moduleRepository;
        protected readonly IClassroomRepository classroomRepository;
        protected readonly IEnrollmentRepository enrollmentRepository;

        public BaseDatabaseTests(IMapper mapper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseNpgsql("User ID=postgres;Password=adm123;Host=localhost;Port=5432;Database=school;Pooling=true;Connection Lifetime=0").Options;
            this.dbContext = new SchoolDbContext(dbContextOptions);
            this.repositoryFactory = new RepositoryDatabaseAbstractFactory(this.dbContext, mapper);
            this.levelRepository = this.repositoryFactory.CreateLevelRepository();
            this.moduleRepository = this.repositoryFactory.CreateModuleRepository();
            this.classroomRepository = this.repositoryFactory.CreateClassroomRepository();
            this.enrollmentRepository = this.repositoryFactory.CreateEnrollmentRepository();
            this.enrollStudent = new EnrollStudent(this.repositoryFactory, mapper);
            this.getEnrollment = new GetEnrollment(this.repositoryFactory, mapper);
        }

        public void Dispose()
        {
            var task = Task.Run(async () => await this.DisposeAsync());
            task.Wait();
            this.dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task DisposeAsync()
        {
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from levels");
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from modules");
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from classrooms");
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from invoice_events");
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from invoices");
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from enrollments");
            await this.dbContext.Database.ExecuteSqlRawAsync("delete from students");
        }
    }
}
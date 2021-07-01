namespace CleanArch.School.UnitTests
{
    using Application.Factory;
    using Application.Repository;
    using Application.UseCase;
    using AutoMapper;
    using Domain.Entity;
    using Infrastructure.Factory;

    // ReSharper disable InconsistentNaming
    public abstract class BaseInMemoryTests : BaseTests
    {
        protected readonly EnrollStudent enrollStudent;
        protected readonly GetEnrollment getEnrollment;
        protected readonly IRepositoryAbstractFactory repositoryFactory;
        protected readonly ILevelRepository levelRepository;
        protected readonly IModuleRepository moduleRepository;
        protected readonly IClassroomRepository classroomRepository;
        protected readonly IEnrollmentRepository enrollmentRepository;

        protected BaseInMemoryTests()
        {
            var mapper = new MapperConfiguration(
                cfg =>
                    cfg.AddMaps(
                        typeof(Enrollment).Assembly,
                        typeof(Infrastructure.Database.Data.Enrollment).Assembly,
                        typeof(PayInvoice).Assembly)).CreateMapper();
            this.repositoryFactory = new RepositoryMemoryAbstractFactory();
            this.levelRepository = this.repositoryFactory.CreateLevelRepository();
            this.moduleRepository = this.repositoryFactory.CreateModuleRepository();
            this.classroomRepository = this.repositoryFactory.CreateClassroomRepository();
            this.enrollmentRepository = this.repositoryFactory.CreateEnrollmentRepository();
            this.enrollStudent = new EnrollStudent(this.repositoryFactory, mapper);
            this.getEnrollment = new GetEnrollment(this.repositoryFactory, mapper);
        }
    }
}
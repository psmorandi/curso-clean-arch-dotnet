namespace CleanArch.School.UnitTests
{
    using Application.Adapter.Factory;
    using Application.Domain.Factory;
    using Application.Domain.Repository;
    using Application.Domain.UseCase;
    using AutoMapper;

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

        protected BaseInMemoryTests(IMapper mapper)
        {
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
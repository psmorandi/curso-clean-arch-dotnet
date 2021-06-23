namespace CleanArch.School.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Application.Adapter.Factory;
    using Application.Domain.Entity;
    using Application.Domain.Factory;
    using Application.Domain.Repository;
    using Application.Domain.UseCase;
    using Application.Extensions;
    using AutoMapper;

    // ReSharper disable InconsistentNaming
    public abstract class BaseEnrollmentTests : BaseTests
    {
        protected readonly EnrollStudent enrollStudent;
        protected readonly GetEnrollment getEnrollment;
        protected readonly IRepositoryAbstractFactory repositoryFactory;
        protected readonly ILevelRepository levelRepository;
        protected readonly IModuleRepository moduleRepository;
        protected readonly IClassroomRepository classroomRepository;
        protected readonly IEnrollmentRepository enrollmentRepository;

        protected BaseEnrollmentTests()
        {
            var configuration =
                new MapperConfiguration(cfg => cfg.AddMaps(typeof(Enrollment).Assembly));
            var outputDataMapper = configuration.CreateMapper();
            this.repositoryFactory = new RepositoryMemoryAbstractFactory();
            this.levelRepository = this.repositoryFactory.CreateLevelRepository();
            this.moduleRepository = this.repositoryFactory.CreateModuleRepository();
            this.classroomRepository = this.repositoryFactory.CreateClassroomRepository();
            this.enrollmentRepository = this.repositoryFactory.CreateEnrollmentRepository();
            this.enrollStudent = new EnrollStudent(this.repositoryFactory, outputDataMapper);
            this.getEnrollment = new GetEnrollment(this.repositoryFactory, outputDataMapper);
        }

        protected async Task<EnrollStudentInputData> CreateEnrollmentRequest(string cpf, string level, string module, string classroom, int installments = 1)
        {
            var minimumAge = (await this.moduleRepository.FindByCode(level, module)).MinimumAge;
            var classCode = (await this.classroomRepository.FindByCode(level, module, classroom)).Code;
            return new EnrollStudentInputData
                   {
                       StudentName = $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                       StudentCpf = cpf,
                       StudentBirthday = DateTime.Now.Date.AddYears(-minimumAge),
                       Level = level,
                       Module = module,
                       Class = classCode,
                       Installments = installments
                   };
        }

        protected async Task<EnrollStudentOutputData> CreateRandomEnrollment(DateOnly issueDate)
        {
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this .moduleRepository.Save(new Module("EM", "1", "1o Ano", 15, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "1", "A", 12);
            return await this.enrollStudent.Execute(enrollmentRequest, issueDate);
        }

        protected string CreateEnrollmentWith(DateOnly issueDate)
        {
            var level = new Level("EM", "Ensino Médio");
            this.levelRepository.Save(level);
            var module = new Module("EM", "1", "1o Ano", 15, 17000);
            this.moduleRepository.Save(module);
            var classroom = new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12));
            this.classroomRepository.Save(classroom);
            var student = new Student(
                $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                "755.525.774-26",
                DateTime.Now.Date.AddYears(-15));
            var enrollment = new Enrollment(student, level, module, classroom, 1, issueDate);
            this.enrollmentRepository.Save(enrollment);
            return enrollment.Code.Value;
        }

        protected async Task<GetEnrollmentOutputData> GetEnrollment(string code, DateOnly refDate) => await this.getEnrollment.Execute(code, refDate);
    }
}
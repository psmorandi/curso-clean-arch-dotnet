namespace CleanArch.School.UnitTests
{
    using System;
    using Application;
    using Application.Extensions;

    // ReSharper disable InconsistentNaming
    public abstract class BaseEnrollmentTests : BaseTests
    {
        protected readonly EnrollStudent enrollStudent;
        protected readonly ILevelRepository levelRepository;
        protected readonly IModuleRepository moduleRepository;
        protected readonly IClassroomRepository classroomRepository;
        protected readonly IEnrollmentRepository enrollmentRepository;

        protected BaseEnrollmentTests()
        {
            this.levelRepository = new LevelRepositoryMemory();
            this.moduleRepository = new ModuleRepositoryMemory();
            this.classroomRepository = new ClassroomRepositoryMemory();
            this.enrollmentRepository = new EnrollmentRepositoryMemory();
            this.enrollStudent = new EnrollStudent(this.enrollmentRepository, this.levelRepository, this.moduleRepository, this.classroomRepository);
        }

        protected EnrollmentRequest CreateEnrollmentRequest(string cpf, string level, string module, string classroom, int installments = 1)
        {
            var minimumAge = this.moduleRepository.FindByCode(level, module).MinimumAge;
            var classCode = this.classroomRepository.FindByCode(level, module, classroom).Code;
            return new EnrollmentRequest
                   {
                       StudentName = $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                       Cpf = cpf,
                       Birthday = DateTime.Now.Date.AddYears(-minimumAge),
                       Level = level,
                       Module = module,
                       Class = classCode,
                       Installments = installments
                   };
        }
    }
}
namespace CleanArch.School.UnitTests
{
    using System;
    using Application;
    using Application.Extensions;

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
            this.repositoryFactory = new RepositoryMemoryAbstractFactory();
            this.levelRepository = this.repositoryFactory.CreateLevelRepository();
            this.moduleRepository = this.repositoryFactory.CreateModuleRepository();
            this.classroomRepository = this.repositoryFactory.CreateClassroomRepository();
            this.enrollmentRepository = this.repositoryFactory.CreateEnrollmentRepository();
            this.enrollStudent = new EnrollStudent(this.repositoryFactory);
            this.getEnrollment = new GetEnrollment(this.repositoryFactory);
        }

        protected EnrollStudentInputData CreateEnrollmentRequest(string cpf, string level, string module, string classroom, int installments = 1)
        {
            var minimumAge = this.moduleRepository.FindByCode(level, module).MinimumAge;
            var classCode = this.classroomRepository.FindByCode(level, module, classroom).Code;
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

        protected Enrollment CreateRandomEnrollment()
        {
            var today = DateTime.Now.Date;
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "1", "1o Ano", 15, 17000));
            this.classroomRepository.Save(new Classroom("EM", "1", "A", 2, today, today.AddMonths(6)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "1", "A", 12);
            return this.enrollStudent.Execute(enrollmentRequest);
        }

        protected Enrollment GetEnrollment(string code) => this.getEnrollment.Execute(new GetEnrollmentRequest { EnrollmentCode = code });
    }
}
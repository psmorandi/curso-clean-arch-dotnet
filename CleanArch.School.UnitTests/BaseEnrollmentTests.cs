namespace CleanArch.School.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Application.UseCase.Data;
    using AutoMapper;
    using Domain.Entity;
    using TypeExtensions;

    // ReSharper disable InconsistentNaming
    public abstract class BaseEnrollmentTests : BaseInMemoryTests
    {
        protected BaseEnrollmentTests()
            : base() { }

        protected Task<EnrollStudentInputData> CreateEnrollmentRequest(string cpf, string level, string module, string classroom, int installments = 1) =>
            this.CreateEnrollmentRequest(
                cpf,
                $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                level,
                module,
                classroom,
                installments);

        protected async Task<EnrollStudentInputData> CreateEnrollmentRequest(string cpf, string name, string level, string module, string classroom, int installments)
        {
            var minimumAge = (await this.moduleRepository.FindByCode(level, module)).MinimumAge;
            var classCode = (await this.classroomRepository.FindByCode(level, module, classroom)).Code;
            return new EnrollStudentInputData
                   {
                       StudentName = name,
                       StudentCpf = cpf,
                       StudentBirthday = DateTime.Now.Date.AddYears(-minimumAge),
                       Level = level,
                       Module = module,
                       Class = classCode,
                       Installments = installments
                   };
        }

        protected async Task<EnrollStudentOutputData> CreateRandomEnrollment(DateOnly issueDate) => await this.CreateRandomEnrollment(issueDate, 12);

        protected async Task<EnrollStudentOutputData> CreateRandomEnrollment(DateOnly issueDate, int installments)
        {
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "1", "1o Ano", 15, 17000));
            await this.classroomRepository.Save(new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12)));
            var enrollmentRequest = await this.CreateEnrollmentRequest("755.525.774-26", "EM", "1", "A", installments);
            return await this.enrollStudent.Execute(enrollmentRequest, issueDate);
        }

        protected async Task<EnrollStudentOutputData> CreateRandomEnrollment(DateOnly issueDate, int installments, string cpf, string name, int modulePrice)
        {
            await this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            await this.moduleRepository.Save(new Module("EM", "1", "1o Ano", 15, modulePrice));
            await this.classroomRepository.Save(new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12)));
            var enrollmentRequest = await this.CreateEnrollmentRequest(cpf, name, "EM", "1", "A", installments);
            return await this.enrollStudent.Execute(enrollmentRequest, issueDate);
        }

        protected async Task<string> CreateEnrollmentWith(DateOnly issueDate)
        {
            var level = new Level("EM", "Ensino Médio");
            await this.levelRepository.Save(level);
            var module = new Module("EM", "1", "1o Ano", 15, 17000);
            await this.moduleRepository.Save(module);
            var classroom = new Classroom("EM", "1", "A", 10, issueDate, issueDate.AddMonths(12));
            await this.classroomRepository.Save(classroom);
            var student = new Student(
                $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(7)}",
                "755.525.774-26",
                DateTime.Now.Date.AddYears(-15));
            var enrollment = new Enrollment(student, level, module, classroom, 1, issueDate);
            await this.enrollmentRepository.Save(enrollment);
            return enrollment.Code.Value;
        }

        protected Task<GetEnrollmentOutputData> GetEnrollment(string code, DateOnly refDate) => this.getEnrollment.Execute(code, refDate);
    }
}
namespace CleanArch.School.UnitTests
{
    using System;
    using Application;
    using Application.Extensions;
    using Xunit;

    public class GetEnrollmentTests
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly GetEnrollment getEnrollment;

        public GetEnrollmentTests()
        {
            this.enrollmentRepository = new EnrollmentRepositoryMemory();
            this.getEnrollment = new GetEnrollment(this.enrollmentRepository);
        }

        // ReSharper disable once InconsistentNaming
        [Fact]
        public void Should_get_enrollment_by_code_with_invoice_balance()
        {
            const string cpf = "275.485.810-50";
            var issueDate = DateTime.UtcNow.Date;
            var student = CreateStudent(cpf, 15);
            var enrollment = CreateEnrollment(student, issueDate);
            this.enrollmentRepository.Save(enrollment);
            var response = this.getEnrollment.Execute(new GetEnrollmentRequest { EnrollmentCode = $"{issueDate.Year}EM1A0001" });
            Assert.Equal(response.Student.Name, enrollment.Student.Name);
            Assert.Equal(student.Cpf.Value, enrollment.Student.Cpf.Value);
            Assert.Equal(response.InvoiceBalance(), new decimal(17000));
        }

        private static Student CreateStudent(string cpf, int age) =>
            new Student(
                $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(4)}",
                cpf,
                DateTime.UtcNow.Date.AddYears(-age));

        private static Enrollment CreateEnrollment(Student student, DateTime? baseDate)
        {
            var date = baseDate ?? DateTime.UtcNow.Date;
            var level = new Level("EM", "Ensino Médio");
            var module = new Module("EM", "1", "1o Ano", student.Age, 17000);
            var classroom = new Classroom("EM", "1", "A", 5, date.Date, date.Date.AddMonths(6));
            return new Enrollment(student, level, module, classroom, 1, date);
        }
    }
}
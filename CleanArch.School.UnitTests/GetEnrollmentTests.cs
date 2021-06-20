namespace CleanArch.School.UnitTests
{
    using System;
    using Application;
    using Application.Extensions;
    using Xunit;

    public class GetEnrollmentTests : BaseEnrollmentTests
    {

        // ReSharper disable once InconsistentNaming
        [Fact]
        public void Should_get_enrollment_by_code_with_invoice_balance()
        {
            const string cpf = "275.485.810-50";
            var issueDate = DateTime.UtcNow.Date;
            var student = CreateStudent(cpf, 15);
            var enrollment = CreateEnrollment(student, issueDate);
            this.enrollmentRepository.Save(enrollment);
            var response = this.getEnrollment.Execute($"{issueDate.Year}EM1A0001");
            Assert.Equal(student.Name.Value, response.StudentName);
            Assert.Equal(student.Cpf.Value, response.StudentCpf);
            Assert.Equal(new decimal(17000), response.InvoiceBalance);
        }

        [Fact]
        public void Should_calculate_due_date_and_return_status_open_or_overdue_for_each_invoice()
        {
            var refDate = DateTime.UtcNow.Date;
            var enrollStudentData = this.CreateRandomEnrollment(refDate);
            var enrollmentData = this.getEnrollment.Execute(enrollStudentData.Code);
            foreach (var invoice in enrollmentData.Invoices)
            {
                Assert.True(invoice.Status == InvoiceStatus.Open);
            }
        }

        private static Student CreateStudent(string cpf, int age) =>
            new(
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
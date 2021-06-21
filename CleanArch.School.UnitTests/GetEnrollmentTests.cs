namespace CleanArch.School.UnitTests
{
    using System;
    using System.Linq;
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
            var issueDate = DateTime.UtcNow.Date.ToDateOnly();
            var student = CreateStudent(cpf, 15);
            var enrollment = CreateEnrollment(student, issueDate);
            this.enrollmentRepository.Save(enrollment);
            var response = this.getEnrollment.Execute($"{issueDate.Year}EM1A0001", issueDate);
            Assert.Equal(student.Name.Value, response.StudentName);
            Assert.Equal(student.Cpf.Value, response.StudentCpf);
            Assert.Equal(new decimal(17000), response.Balance);
        }

        [Fact]
        public void Should_calculate_due_date_and_return_status_open_or_overdue_for_each_invoice()
        {
            var refDate = DateTime.UtcNow.Date.ToDateOnly();
            var enrollStudentData = this.CreateRandomEnrollment(refDate);
            var enrollmentData = this.getEnrollment.Execute(enrollStudentData.Code, refDate);
            foreach (var invoice in enrollmentData.Invoices)
            {
                Assert.True(invoice.Status == InvoiceStatus.Open);
            }
        }

        [Fact]
        public void Should_calculate_penalty_and_interests()
        {
            var today = DateTime.UtcNow.ToDateOnly();
            var refDate = today.AddDays(-5);
            var code = this.CreateEnrollmentWith(refDate);
            var enrollmentData = this.getEnrollment.Execute(code, today);
            var overdueInvoice = enrollmentData.Invoices.ElementAt(0);            
            Assert.Equal(InvoiceStatus.Overdue, overdueInvoice.Status);
            Assert.Equal(InvoicePenalty(overdueInvoice.Balance).Truncate(2), overdueInvoice.Penalty);
            Assert.Equal(InvoiceInterests(overdueInvoice.Balance, 5).Truncate(2), overdueInvoice.Interests);
        }

        private static Student CreateStudent(string cpf, int age) =>
            new(
                $"{StringExtensions.GenerateRandomString(5)} {StringExtensions.GenerateRandomString(4)}",
                cpf,
                DateTime.UtcNow.Date.AddYears(-age));

        private static Enrollment CreateEnrollment(Student student, DateOnly? baseDate)
        {
            var date = baseDate ?? DateTime.UtcNow.ToDateOnly();
            var level = new Level("EM", "Ensino Médio");
            var module = new Module("EM", "1", "1o Ano", student.Age, 17000);
            var classroom = new Classroom("EM", "1", "A", 5, date, date.AddMonths(6));
            return new Enrollment(student, level, module, classroom, 1, date);
        }

        private static decimal InvoicePenalty(decimal amount) => Math.Round(amount * new decimal(0.1), 2).Truncate(2);
        private static decimal InvoiceInterests(decimal amount, int days) => Math.Round(amount * new decimal (days * 0.01), 2).Truncate(2);
    }
}
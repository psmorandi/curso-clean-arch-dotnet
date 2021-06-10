namespace CleanArch.School.UnitTests
{
    using System;
    using System.Linq;
    using CleanArch.School.Application;
    using Xunit;

    public class PayInvoiceTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_pay_enrollment_invoice()
        {
            var today = DateTime.Now.Date;
            this.levelRepository.Save(new Level("EM", "Ensino Médio"));
            this.moduleRepository.Save(new Module("EM", "1", "1o Ano", 15, 17000));
            this.classroomRepository.Save(new Classroom("EM", "1", "A", 2, today, today.AddMonths(6)));
            var enrollmentRequest = this.CreateEnrollmentRequest("755.525.774-26", "EM", "1", "A", 12);
            var enrollment = this.enrollStudent.Execute(enrollmentRequest);
            var invoiceToPay = enrollment.Invoices.Single(i => i.Month == 1 && i.Year == today.Year);
            var expectedBalanceAfterPayment = enrollment.InvoiceBalance() - invoiceToPay.Amount;
            var payInvoiceRequest = new PayInvoiceRequest
                                    {
                                        Code = enrollment.Code.Value,
                                        Month = invoiceToPay.Month,
                                        Year = invoiceToPay.Year,
                                        Amount = invoiceToPay.Amount
                                    };
            var payInvoice = new PayInvoice(this.enrollmentRepository);
            payInvoice.Execute(payInvoiceRequest);
            Assert.Equal(expectedBalanceAfterPayment, enrollment.InvoiceBalance());
        }
    }
}
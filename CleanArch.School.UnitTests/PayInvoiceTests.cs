namespace CleanArch.School.UnitTests
{
    using System;
    using System.Linq;
    using Application;
    using Xunit;

    public class PayInvoiceTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_pay_enrollment_invoice()
        {
            var date = DateTime.UtcNow.Date;
            var enrollment = this.CreateRandomEnrollment(date);
            var invoiceToPay = enrollment.Invoices.Single(i => i.DueDate.Month == date.Month && i.DueDate.Year == date.Year);
            var expectedBalanceAfterPayment = enrollment.Invoices.Sum(i => i.Amount) - invoiceToPay.Amount;
            var payInvoiceRequest = new PayInvoiceInputData
                                    {
                                        Code = enrollment.Code,
                                        Month = invoiceToPay.DueDate.Month,
                                        Year = invoiceToPay.DueDate.Year,
                                        Amount = invoiceToPay.Amount
                                    };
            var payInvoice = new PayInvoice(this.repositoryFactory);
            payInvoice.Execute(payInvoiceRequest);
            var updatedEnrollment = this.GetEnrollment(enrollment.Code);
            Assert.Equal(expectedBalanceAfterPayment, updatedEnrollment.InvoiceBalance);
        }
    }
}
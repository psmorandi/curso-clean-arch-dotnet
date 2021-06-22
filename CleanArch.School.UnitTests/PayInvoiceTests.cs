namespace CleanArch.School.UnitTests
{
    using System;
    using System.Linq;
    using Application;
    using Application.Domain.UseCase;
    using Application.Extensions;
    using Xunit;

    public class PayInvoiceTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_pay_enrollment_invoice()
        {
            var date = DateTime.UtcNow.ToDateOnly();
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
            var updatedEnrollment = this.GetEnrollment(enrollment.Code, date);
            Assert.Equal(expectedBalanceAfterPayment, updatedEnrollment.Balance);
        }

        [Fact]
        public void Should_pay_overdue_invoice()
        {
            var refDate = DateTime.UtcNow.Date.AddDays(-5).ToDateOnly();
            var code = this.CreateEnrollmentWith(refDate);
            var payInvoice = new PayInvoice(this.repositoryFactory);
            var enrollment = this.GetEnrollment(code, refDate);
            var balanceBeforePayment = enrollment.Balance;
            var invoiceToPay = enrollment.Invoices.ElementAt(0);
            var payInvoiceRequest = new PayInvoiceInputData
                                    {
                                        Code = code,
                                        Month = refDate.Month,
                                        Year = refDate.Year,
                                        Amount = invoiceToPay.Amount + invoiceToPay.Interests + invoiceToPay.Penalty
                                    };
            payInvoice.Execute(payInvoiceRequest);
            var updatedEnrollment = this.GetEnrollment(enrollment.Code, refDate);
            Assert.Equal(balanceBeforePayment - invoiceToPay.Amount, updatedEnrollment.Balance);
        }
    }
}
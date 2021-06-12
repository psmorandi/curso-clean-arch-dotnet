namespace CleanArch.School.UnitTests
{
    using System.Linq;
    using Application;
    using Xunit;

    public class PayInvoiceTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_pay_enrollment_invoice()
        {
            var enrollment = this.CreateRandomEnrollment();
            var invoiceToPay = enrollment.Invoices.Single(i => i.Month == 1 && i.Year == enrollment.IssueDate.Year);
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
            var updatedEnrollment = this.GetEnrollment(enrollment.Code.Value);
            Assert.Equal(expectedBalanceAfterPayment, updatedEnrollment.InvoiceBalance());
        }
    }
}
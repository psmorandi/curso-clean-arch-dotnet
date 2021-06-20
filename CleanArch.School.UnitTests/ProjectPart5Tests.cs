namespace CleanArch.School.UnitTests
{
    using CleanArch.School.Application;
    using System;
    using Xunit;

    public class ProjectPart5Tests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_calculate_due_date_and_return_status_open_or_overdue_for_each_invoice()
        {
            var refDate = DateTime.UtcNow.Date;
            var outputData = this.CreateRandomEnrollment(refDate);
            foreach (var invoice in outputData.Invoices)
            {
                Assert.True(invoice.Status == InvoiceStatus.Open);
            }
        }
    }
}
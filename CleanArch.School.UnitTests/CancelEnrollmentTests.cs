namespace CleanArch.School.UnitTests
{
    using Application;
    using System;
    using Xunit;

    public class CancelEnrollmentTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_cancel_enrollment()
        {
            var enrollment = this.CreateRandomEnrollment(DateTime.UtcNow.Date);
            var enrollCode = enrollment.Code;
            var cancelRequest = new CancelEnrollmentRequest { EnrollmentCode = enrollCode };
            var cancelEnrollment = new CancelEnrollment(this.repositoryFactory);
            cancelEnrollment.Execute(cancelRequest);
            var cancelledEnrollment = this.GetEnrollment(enrollCode);
            Assert.True(cancelledEnrollment.Status == EnrollStatus.Cancelled);
        }
    }
}
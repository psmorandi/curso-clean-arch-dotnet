namespace CleanArch.School.UnitTests
{
    using Application;
    using CleanArch.School.Application.Extensions;
    using System;
    using Application.Domain.Entity;
    using Xunit;

    public class CancelEnrollmentTests : BaseEnrollmentTests
    {
        [Fact]
        public void Should_cancel_enrollment()
        {
            var refDate = DateTime.UtcNow.ToDateOnly();
            var enrollment = this.CreateRandomEnrollment(refDate);
            var enrollCode = enrollment.Code;
            var cancelEnrollment = new CancelEnrollment(this.repositoryFactory);
            cancelEnrollment.Execute(enrollCode);
            var cancelledEnrollment = this.GetEnrollment(enrollCode, refDate);
            Assert.True(cancelledEnrollment.Status == EnrollStatus.Cancelled);
        }
    }
}
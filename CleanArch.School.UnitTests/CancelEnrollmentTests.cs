namespace CleanArch.School.UnitTests
{
    using System;
    using Application.Domain.Entity;
    using Application.Domain.UseCase;
    using Application.Extensions;
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